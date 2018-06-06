using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using RFI.MenuCardsAggregator.Services.Model;

namespace RFI.MenuCardsAggregator.Services.Services
{
    public class TustoRestaurantService : BaseRestaurantService
    {
        public override string RestaurantName => "Tusto restaurant";

        public TustoRestaurantService()
        {
            Uri = "http://titanium.tusto.cz/tydenni-menu/";
        }

        public TustoRestaurantService(IHttpService httpService)
            : base(httpService)
        { }

        public override async Task<MenuCard> GetMenuCardAsync()
        {
            var menuCard = new MenuCard(RestaurantName, Uri);
            var htmlDocument = await GetHtmlDocumentAsync();

            foreach (var dayTableNode in htmlDocument.DocumentNode.SelectNodes("*//table[@class='menu']"))
            {
                var dayMenu = GetMenuForDay(dayTableNode);
                menuCard.DayMenus.Add(dayMenu);
            }

            return menuCard;
        }

        private static DayMenu GetMenuForDay(HtmlNode dayTableNode)
        {
            var dayMenu = new DayMenu
            {
                Date = GetDate(dayTableNode)
            };

            foreach (var trNode in dayTableNode.GetChildElements().Skip(1))
            {
                dayMenu.Foods.Add(new Food
                {
                    Name = GetFoodName(trNode.GetChildElements().First())
                });
            }

            return dayMenu;
        }

        private static DateTime GetDate(HtmlNode dayTableNode)
        {
            var trNode = dayTableNode.GetChildElements().First();
            var tdNode = trNode.GetChildElements().First();

            // TODO - extract to the base class
            // Create special class for regex constants
            const string reMess = ".*?"; // Non-greedy match on filler
            const string reDay = "((?:(?:[0-2]?\\d{1})|(?:[3][01]{1})))(?![\\d])";
            const string reMonth = "((?:(?:[0]?[1-9])|(?:[1]{1}[012]{1})))(?![\\d])";

            var r = new Regex(reMess + reDay + reMess + reMonth + reMess, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var m = r.Match(tdNode.InnerText);
            if (m.Success)
            {
                var day = m.Groups[1].ToString();
                var month = m.Groups[2].ToString();

                return CreateDate($"{day}.{month}.{DateTime.Now.Year}");
            }

            return DateTime.MinValue;
        }

        private static string GetFoodName(HtmlNode foodNameTdNode) => foodNameTdNode.InnerText[1] == ')' ? foodNameTdNode.InnerText.Substring(3) : foodNameTdNode.InnerText;
    }
}