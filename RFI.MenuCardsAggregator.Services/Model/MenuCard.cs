using System;
using System.Collections.Generic;

namespace RFI.MenuCardsAggregator.Services.Model
{
    public class MenuCard
    {
        public string RestaurantName { get; set; }

        public string RestaurantUri { get; set; }

        public List<DayMenu> DayMenus { get; set; }

        public string MenuImageUri { get; set; }

        public MenuCard(string restaurantName, string restaurantUri)
        {
            RestaurantName = restaurantName;
            RestaurantUri = restaurantUri;
            DayMenus = new List<DayMenu>();
        }
    }
}
