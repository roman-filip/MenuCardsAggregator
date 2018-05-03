using System;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using RFI.MenuCardsAggregator.Services.Model;

namespace RFI.MenuCardsAggregator.Services.Services
{
    public class MyFoodRestaurantService : BaseRestaurantService
    {
        protected override string CurrencySymbol
        {
            get { return " Kč"; }
        }

        public override string RestaurantName
        {
            get { return "My Food"; }
        }

        public MyFoodRestaurantService()
        {
            Uri = @"http://www.myfoodmarket.cz/brno-holandska/";
        }

        public MyFoodRestaurantService(IHttpService httpService)
            : base(httpService)
        { }

        public async override Task<MenuCard> GetMenuCardAsync()
        {
            var menuCard = new MenuCard(RestaurantName, Uri);
            var htmlDocument = await GetHtmlDocumentAsync();
            var date = GetMondayDate(htmlDocument);
            ProcessDays(menuCard, htmlDocument, date);

            return menuCard;
        }

        private void ProcessDays(MenuCard menuCard, HtmlDocument htmlDocument, DateTime date)
        {
            var jidlaDivNode = htmlDocument.DocumentNode.QuerySelector(".jidla");
            foreach (var dayFoodsDivNode in jidlaDivNode.GetChildElements())
            {
                var dayMenu = GetDayMenu(date, dayFoodsDivNode);
                if (dayMenu == null)
                {
                    return;
                }
                menuCard.DayMenus.Add(dayMenu);
                date = date.AddDays(1);
            }
        }

        private DayMenu GetDayMenu(DateTime date, HtmlNode dayFoodsDivNode)
        {
            var dayMenu = new DayMenu { Date = date };
            var foodsLiNode = dayFoodsDivNode.SelectNodes("*//li");
            if (foodsLiNode == null)
            {
                return null;
            }
            foreach (var foodLiNode in foodsLiNode)
            {
                var food = new Food
                {
                    Name = foodLiNode.ChildNodes[0].InnerText,
                    Price = GetPriceFromHtmlNode(foodLiNode.ChildNodes[1])
                };
                dayMenu.Foods.Add(food);
            }
            return dayMenu;
        }

        private static DateTime GetMondayDate(HtmlDocument htmlDocument)
        {
            var daysDivNode = htmlDocument.DocumentNode.QuerySelector(".dny");
            var datesSpanNode = daysDivNode.GetChildElements().First().GetChildElements().First().GetChildElements().First();
            var datesStr = GetStringFomHtmlNode(datesSpanNode);
            var sundayDateStr = datesStr.Split('-')[1].Replace(" ", "").Replace(")", "");
            var sundayDate = CreateDate(sundayDateStr);
            return sundayDate.AddDays(-6);
        }
    }
}
