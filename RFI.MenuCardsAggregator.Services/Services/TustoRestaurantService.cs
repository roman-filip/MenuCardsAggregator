using System;
using System.Linq;
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
            
            foreach (var trNode in dayTableNode.GetChildElements())
            {
                
            }

            return dayMenu;
        }

        private static DateTime GetDate(HtmlNode dayTableNode)
        {
            var trNode = dayTableNode.GetChildElements().First();
            var tdNode = trNode.GetChildElements().First();

            return DateTime.MinValue;
        }
    }
}