using System;
using System.Threading.Tasks;
using RFI.MenuCardsAggregator.Services.Model;

namespace RFI.MenuCardsAggregator.Services.Services
{
    public class UTrechCertuRestaurantService : BaseRestaurantService
    {
        public override string RestaurantName => "U Třech čertů";

        protected override string CurrencySymbol => "Kč";

        public UTrechCertuRestaurantService()
        {
            Uri = "http://ucertu.cz/nabidka-starobrnenska";
        }

        public UTrechCertuRestaurantService(IHttpService httpService)
            : base(httpService)
        { }

        public override async Task<MenuCard> GetMenuCardAsync()
        {
            var htmlDocument = await GetHtmlDocumentAsync();
            var menuCard = new MenuCard(RestaurantName, Uri);

            var menuTable = htmlDocument.GetElementbyId("menuTable");
            DayMenu dayMenu = null;
            foreach (var tr in menuTable.ChildNodes)
            {
                if (tr.HasClass("menu_line"))
                {
                    dayMenu = new DayMenu { Date = GetDate(GetStringFomHtmlNode(tr.ChildNodes[0])) };
                    menuCard.DayMenus.Add(dayMenu);
                }
                else
                {
                    dayMenu.Foods.Add(new Food
                    {
                        Name = GetStringFomHtmlNode(tr.ChildNodes[0]),
                        Price = GetPriceFromHtmlNode(tr.ChildNodes[1])
                    });
                }
            }

            return menuCard;
        }

        // TODO - move to base class
        private DateTime GetDate(string dayName)
        {
            switch (dayName)
            {
                case "Pondělí":
                    return DateTime.Today.AddDays(-(double)DateTime.Today.DayOfWeek + 1);
                case "Úterý":
                    return DateTime.Today.AddDays(-(double)DateTime.Today.DayOfWeek + 2);
                case "Středa":
                    return DateTime.Today.AddDays(-(double)DateTime.Today.DayOfWeek + 3);
                case "Čtvrtek":
                    return DateTime.Today.AddDays(-(double)DateTime.Today.DayOfWeek + 4);
                case "Pátek":
                    return DateTime.Today.AddDays(-(double)DateTime.Today.DayOfWeek + 5);
                default:
                    throw new ArgumentOutOfRangeException("Unsupported day name");
            }
        }
    }
}
