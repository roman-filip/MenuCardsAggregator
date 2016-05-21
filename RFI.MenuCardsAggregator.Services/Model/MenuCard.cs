using System.Collections.Generic;

namespace RFI.MenuCardsAggregator.Services.Model
{
    public class MenuCard
    {
        public string RestaurantName { get; set; }

        public List<DayMenu> DayMenus { get; set; }

        public MenuCard(string restaurantName)
        {
            RestaurantName = restaurantName;
            DayMenus = new List<DayMenu>();
        }
    }
}
