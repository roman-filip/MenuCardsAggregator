using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using RFI.MenuCardsAggregator.Services.Model;

namespace RFI.MenuCardsAggregator.Services.Services
{
    public class KometaRestaurantService : BaseRestaurantService
    {
        public override string RestaurantName
        {
            get { return "Kometa Pub"; }
        }

        public KometaRestaurantService()
        {
            Uri = "https://arena.kometapub.cz/tydenni-menu.php";
        }

        public KometaRestaurantService(IHttpService httpService)
            : base(httpService)
        { }

        public async override Task<MenuCard> GetMenuCardAsync()
        {
            var menuCard = new MenuCard(RestaurantName, Uri);

            var htmlDocument = await GetHtmlDocumentAsync();
            var dayH3Nodes = htmlDocument.DocumentNode.QuerySelectorAll(".subh3");
            foreach (var dayH3Node in dayH3Nodes)
            {
                var dayMenu = new DayMenu();
                FillInDate(dayMenu, dayH3Node);

                var foodsTableNode = dayH3Node.NextSiblingElement();
                FillInFoods(dayMenu, foodsTableNode);

                menuCard.DayMenus.Add(dayMenu);
            }
            return menuCard;
        }

        private void FillInDate(DayMenu dayMenu, HtmlNode dayH3Node)
        {
            var htmlDate = GetStringFomHtmlNode(dayH3Node);

            const string reMess = ".*?"; // Non-greedy match on filler
            const string reDay = "((?:(?:[0-2]?\\d{1})|(?:[3][01]{1})))(?![\\d])";
            const string reMonthName = @"([\p{L}]+)";
            const string reYear = "((?:(?:[1]{1}\\d{1}\\d{1}\\d{1})|(?:[2]{1}\\d{3})))(?![\\d])";

            var r = new Regex(reMess + reDay + reMess + reMonthName + reMess + reYear, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var m = r.Match(htmlDate);
            if (m.Success)
            {
                var day = m.Groups[1].ToString();
                var monthName = m.Groups[2].ToString();
                var year = m.Groups[3].ToString();

                dayMenu.Date = CreateDate(day, monthName, year);
            }
        }

        private void FillInFoods(DayMenu dayMenu, HtmlNode foodsTableNode)
        {
            foreach (var foodTrNode in foodsTableNode.GetChildElements().Skip(1))
            {
                var foodNameTdNode = foodTrNode.GetChildElements().First();
                var priceTdNode = foodNameTdNode.NextSiblingElement();
                var food = new Food
                {
                    Name = GetStringFomHtmlNode(foodNameTdNode),
                    Price = priceTdNode.InnerText == "&nbsp;" ? 0 : GetPriceFromHtmlNode(priceTdNode)
                };
                dayMenu.Foods.Add(food);
            }
        }
    }
}
