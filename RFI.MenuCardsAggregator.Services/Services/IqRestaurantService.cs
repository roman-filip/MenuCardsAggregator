using System;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using RFI.MenuCardsAggregator.Services.Model;

namespace RFI.MenuCardsAggregator.Services.Services
{
    public class IqRestaurantService : BaseRestaurantService
    {
        protected override string CurrencySymbol
        {
            get { return ",-"; }
        }

        public override string RestaurantName
        {
            get { return "IQ Restaurant"; }
        }

        public IqRestaurantService()
        {
            Uri = @"http://www.iqrestaurant.cz/brno/getData.svc?type=brnoMenuHTML";
        }

        public IqRestaurantService(IHttpService httpService)
            : base(httpService)
        { }

        public override async Task<MenuCard> GetMenuCardAsync()
        {
            var menuCard = new MenuCard(RestaurantName, Uri);

            var htmlDocument = await GetHtmlDocumentAsync();

            var dateDivNodes = htmlDocument.DocumentNode.QuerySelectorAll(".date");
            foreach (var dateDivNode in dateDivNodes)
            {
                var dayMenu = new DayMenu();
                FillInDate(dayMenu, dateDivNode);

                var foodsDlNode = dateDivNode.NextSiblingElement().NextSiblingElement();
                FillInFoods(dayMenu, foodsDlNode, false);

                var weekFoodsDlNode = foodsDlNode.NextSiblingElement().NextSiblingElement().NextSiblingElement();
                FillInFoods(dayMenu, weekFoodsDlNode, true);

                menuCard.DayMenus.Add(dayMenu);
            }
            return menuCard;
        }

        private void FillInDate(DayMenu dayMenu, HtmlNode dateDivNode)
        {
            var day = GetStringFomHtmlNode(dateDivNode.QuerySelector(".day"));
            var month = GetStringFomHtmlNode(dateDivNode.QuerySelector(".month"));

            dayMenu.Date = CreateDate(day, month, DateTime.Now.Year.ToString());
        }

        private void FillInFoods(DayMenu dayMenu, HtmlNode foodsDlNode, bool isWeekFood)
        {
            foreach (var dtNode in foodsDlNode.GetChildElements().Where(element => element.Name == "dt"))
            {
                dtNode.RemoveChild(dtNode.GetChildElements().ElementAt(0)); // Remove span element
                var ddNode = dtNode.NextSiblingElement();
                var food = new Food
                {
                    Name = GetStringFomHtmlNode(dtNode),
                    Price = GetPriceFromHtmlNode(ddNode),
                    IsWeekFood = isWeekFood
                };
                dayMenu.Foods.Add(food);
            }
        }
    }
}
