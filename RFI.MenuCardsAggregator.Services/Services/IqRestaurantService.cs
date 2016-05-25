using System;
using System.Globalization;
using System.Linq;
using HtmlAgilityPack;
using RFI.MenuCardsAggregator.Services.Model;

namespace RFI.MenuCardsAggregator.Services.Services
{
    public class IqRestaurantService : IRestaurantService
    {
        private readonly IHttpService _httpService;

        private readonly CultureInfo _czCultureInfo = new CultureInfo("cs-CZ");

        public string RestaurantName
        {
            get { return "IQ Restaurant"; }
        }

        public string Uri { get; set; }

        public IqRestaurantService()
        {
            _httpService = new HttpService();

            Uri = @"http://www.iqrestaurant.cz/brno/getData.svc?type=brnoMenuHTML";
        }

        public IqRestaurantService(IHttpService httpService)
        {
            _httpService = httpService;


            _httpService = new HttpService();

            Uri = @"http://www.iqrestaurant.cz/brno/getData.svc?type=brnoMenuHTML";
        }

        public MenuCard GetMenuCard()
        {
            var menuCard = new MenuCard(RestaurantName);

            var htmlDocument = GetHtmlDocument();

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

        private HtmlDocument GetHtmlDocument()
        {
            var webPage = _httpService.Get(Uri);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(webPage);
            return htmlDocument;
        }

        private void FillInDate(DayMenu dayMenu, HtmlNode dateDivNode)
        {
            var day = GetIntFomHtmlNode(dateDivNode.QuerySelector(".day"));
            var month = GetStringFomHtmlNode(dateDivNode.QuerySelector(".month"));
            var stringDate = string.Format("{0} {1} {2}", day, month, DateTime.Now.Year);

            dayMenu.Date = DateTime.ParseExact(stringDate, "d MMMM yyyy", _czCultureInfo);
        }

        private static void FillInFoods(DayMenu dayMenu, HtmlNode foodsDlNode, bool isWeekFood)
        {
            foreach (var dtNode in foodsDlNode.GetChildElements().Where(element => element.Name == "dt"))
            {
                dtNode.RemoveChild(dtNode.GetChildElements().ElementAt(0)); // Remove span element

                var ddNode = dtNode.NextSiblingElement();
                var ddNodeInnerText = GetStringFomHtmlNode(ddNode);
                var stringPrice = ddNodeInnerText.Substring(0, ddNodeInnerText.IndexOf(','));
                var food = new Food
                {
                    Name = GetStringFomHtmlNode(dtNode),
                    Price = Convert.ToDecimal(stringPrice),
                    IsWeekFood = isWeekFood
                };
                dayMenu.Foods.Add(food);
            }
        }

        private static int GetIntFomHtmlNode(HtmlNode node)
        {
            var nodeContent = node.InnerText;
            return Convert.ToInt32(nodeContent.Trim());
        }

        private static string GetStringFomHtmlNode(HtmlNode node)
        {
            return node.InnerText.Trim();
        }
    }
}
