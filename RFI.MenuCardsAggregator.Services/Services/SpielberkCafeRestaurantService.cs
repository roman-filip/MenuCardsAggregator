using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using RFI.MenuCardsAggregator.Services.Model;

namespace RFI.MenuCardsAggregator.Services.Services
{
    public class SpielberkCafeRestaurantService : BaseRestaurantService
    {
        private readonly Dictionary<char, decimal> _menuPrices = new Dictionary<char, decimal>();

        protected override string CurrencySymbol
        {
            get { return ",- Kč"; }
        }

        public override string RestaurantName
        {
            get { return "Spielberk café"; }
        }

        public SpielberkCafeRestaurantService()
        {
            Uri = @"http://www.spielberkcafe.cz/denni_menu.html";
        }

        public SpielberkCafeRestaurantService(IHttpService httpService)
            : base(httpService)
        { }

        public async override Task<MenuCard> GetMenuCardAsync()
        {
            var menuCard = new MenuCard(RestaurantName, Uri);

            var htmlDocument = await GetHtmlDocumentAsync();

            PrepareFoodPrices(htmlDocument);

            var date = GetMondayDate(htmlDocument);
            menuCard.DayMenus.Add(GetMenuForDay(htmlDocument, "Pondělí", date));

            date = date.AddDays(1);
            menuCard.DayMenus.Add(GetMenuForDay(htmlDocument, "Úterý", date));

            date = date.AddDays(1);
            menuCard.DayMenus.Add(GetMenuForDay(htmlDocument, "Středa", date));

            date = date.AddDays(1);
            menuCard.DayMenus.Add(GetMenuForDay(htmlDocument, "Čtvrtek", date));

            date = date.AddDays(1);
            menuCard.DayMenus.Add(GetMenuForDay(htmlDocument, "Pátek", date));

            return menuCard;
        }

        private static DateTime GetMondayDate(HtmlDocument htmlDocument)
        {
            var dailyMenuNode = htmlDocument.DocumentNode.SelectSingleNode(".//p[text()='Denní menu']");
            var dateNode = dailyMenuNode.ParentNode.NextSiblingElement().GetChildElements().First();
            var dateStr = GetStringFomHtmlNode(dateNode);
            var dateParts = dateStr.Split(' ');
            var date = CreateDate(dateParts[0] + DateTime.Now.Year);

            return date;
        }

        private void PrepareFoodPrices(HtmlDocument htmlDocument)
        {
            var menuPricesTitleNode = htmlDocument.DocumentNode.SelectSingleNode(".//p[text()='Cena menu:']");
            var menuPricesNode = menuPricesTitleNode.NextSiblingElement();
            var menuPricesStr = GetStringFomHtmlNode(menuPricesNode);
            var menuPrices = menuPricesStr.Split(new[] { CurrencySymbol }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var menuPrice in menuPrices)
            {
                var menuPriceParts = menuPrice.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var foodPrice = Convert.ToDecimal(menuPriceParts.Last());
                foreach (var menuPricePart in menuPriceParts)
                {
                    if (menuPricePart.Length == 1 && char.IsLetter(menuPricePart, 0))
                    {   // Food code
                        _menuPrices[menuPricePart[0]] = foodPrice;
                    }
                    else if (menuPricePart == "polévky")
                    {   // Soup
                        _menuPrices['P'] = foodPrice;
                    }
                }
            }
        }

        private DayMenu GetMenuForDay(HtmlDocument htmlDocument, string dayName, DateTime date)
        {
            var dayMenu = new DayMenu { Date = date };

            var dayNode = htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//p[text()='{0}']", dayName));
            var pNode = dayNode.NextSiblingElement();
            var nodeText = GetStringFomHtmlNode(pNode);
            while (!string.IsNullOrEmpty(nodeText))
            {
                Food food;
                if (nodeText[1] == '.')
                {   // Normal food with food code
                    food = new Food
                    {
                        Name = nodeText.Substring(3),
                        Price = _menuPrices[nodeText[0]]
                    };
                }
                else
                {   // Food is soup
                    food = new Food
                    {
                        Name = nodeText,
                        Price = _menuPrices['P']
                    };
                }
                dayMenu.Foods.Add(food);

                pNode = pNode.NextSiblingElement();
                nodeText = GetStringFomHtmlNode(pNode);
            }

            return dayMenu;
        }
    }
}
