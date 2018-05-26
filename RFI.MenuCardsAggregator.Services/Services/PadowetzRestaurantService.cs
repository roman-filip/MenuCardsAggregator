using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using RFI.MenuCardsAggregator.Services.Model;

namespace RFI.MenuCardsAggregator.Services.Services
{
    public class PadowetzRestaurantService : BaseRestaurantService
    {
        public override string RestaurantName => "Padowetz restaurant";

        public PadowetzRestaurantService()
        {
            Uri = "http://restaurant-padowetz.cz/poledni-menu.htm";
        }

        public PadowetzRestaurantService(IHttpService httpService)
            : base(httpService)
        { }

        public override async Task<MenuCard> GetMenuCardAsync()
        {
            var menuCard = new MenuCard(RestaurantName, Uri);
            var htmlDocument = await GetHtmlDocumentAsync();

            menuCard.DayMenus.Add(GetMenuForDay(htmlDocument, "t_pondeli"));
            menuCard.DayMenus.Add(GetMenuForDay(htmlDocument, "t_utery"));
            menuCard.DayMenus.Add(GetMenuForDay(htmlDocument, "t_streda"));
            menuCard.DayMenus.Add(GetMenuForDay(htmlDocument, "t_ctvrtek"));
            menuCard.DayMenus.Add(GetMenuForDay(htmlDocument, "t_patek"));

            return menuCard;
        }

        private DayMenu GetMenuForDay(HtmlDocument htmlDocument, string dayDivId)
        {
            var dayMenu = new DayMenu();

            var dayDiv = htmlDocument.GetElementbyId(dayDivId);
            foreach (var childElement in dayDiv.GetChildElements())
            {
                if (childElement.GetClasses().Contains("loc_datum"))
                {
                    // get date
                    // childElement.InnerText
                }
                else if (childElement.Id == "t_Polevky")
                {
                    
                }
                else if (childElement.Id == "t_Hlavni-chod")
                {
                    
                }
            }

            return dayMenu;
        }
    }
}