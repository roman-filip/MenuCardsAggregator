using System;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using RFI.MenuCardsAggregator.Services.Model;

namespace RFI.MenuCardsAggregator.Services.Services
{
    public class NaTahuRestaurantService : BaseRestaurantService
    {
        private decimal _defaultPrice;

        protected override string CurrencySymbol => ",- Kč";

        public override string RestaurantName => "Na Tahu";

        public NaTahuRestaurantService()
        {
            Uri = @"http://www.na-tahu.cz/tydenni-menu/";
        }

        public NaTahuRestaurantService(IHttpService httpService)
            : base(httpService)
        { }

        public override async Task<MenuCard> GetMenuCardAsync()
        {
            var menuCard = new MenuCard(RestaurantName, Uri);
            var htmlDocument = await GetHtmlDocumentAsync();

            SetDefaultPrice(htmlDocument);

            var date = GetMondayDate(htmlDocument);
            menuCard.DayMenus.Add(GetMenuForDay(htmlDocument, "PONDĚLÍ", date));

            date = date.AddDays(1);
            menuCard.DayMenus.Add(GetMenuForDay(htmlDocument, "ÚTERÝ", date));

            date = date.AddDays(1);
            menuCard.DayMenus.Add(GetMenuForDay(htmlDocument, "STŘEDA", date));

            date = date.AddDays(1);
            menuCard.DayMenus.Add(GetMenuForDay(htmlDocument, "ČTVRTEK", date));

            date = date.AddDays(1);
            menuCard.DayMenus.Add(GetMenuForDay(htmlDocument, "PÁTEK", date));

            return menuCard;
        }

        private void SetDefaultPrice(HtmlDocument htmlDocument)
        {
            var soupDivNode = htmlDocument.DocumentNode.SelectNodes(".//div[contains(., 'cena menu')]").Last();
            var menuPriceNode = soupDivNode.ChildNodes[2];
            _defaultPrice = GetPriceFromHtmlNode(menuPriceNode);
        }

        private static DateTime GetMondayDate(HtmlDocument htmlDocument)
        {
            var centerTopDivNode = htmlDocument.DocumentNode.QuerySelector("#centerTop");
            var menuDateH2Node = centerTopDivNode.GetChildElements().First();
            var datesStr = menuDateH2Node.InnerText;
            var fridayDateStr = datesStr.Split('-')[1];
            var fridayDate = CreateDate(fridayDateStr);
            return fridayDate.AddDays(-4);
        }

        private DayMenu GetMenuForDay(HtmlDocument htmlDocument, string dayName, DateTime date)
        {
            var dayMenu = new DayMenu { Date = date };

            var soupDivNode = htmlDocument.DocumentNode.SelectNodes($".//div[contains(., '{dayName}')]").Last();
            var dayAndSoupStr = GetStringFomHtmlNode(soupDivNode);
            var soupName = dayAndSoupStr.Split(':')[1].Trim();
            dayMenu.Foods.Add(new Food { Name = soupName });

            var foodDivNode = soupDivNode.NextSiblingElement();
            while (foodDivNode != null && !foodDivNode.GetChildElements().Any())
            {
                var foodStr = GetStringFomHtmlNode(foodDivNode);
                if (string.IsNullOrEmpty(foodStr))
                {
                    break;
                }
                var foodName = foodStr.Substring(3);
                var foodPrice = _defaultPrice;

                if (foodName.EndsWith(CurrencySymbol))
                {
                    foodPrice = GetPriceFromHtmlNode(foodDivNode);
                    foodName = foodName.Replace(" " + foodPrice + CurrencySymbol, "");
                }

                dayMenu.Foods.Add(new Food
                {
                    Name = foodName,
                    Price = foodPrice
                });

                foodDivNode = foodDivNode.NextSiblingElement();
            }

            return dayMenu;
        }
    }
}
