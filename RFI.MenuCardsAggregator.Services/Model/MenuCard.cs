using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFI.MenuCardsAggregator.Services.Model
{
    public class MenuCard
    {
        public string RestaurantName { get; set; }

        public List<DayMenu> Menu { get; set; }

        public MenuCard(string restaurantName)
        {
            RestaurantName = restaurantName;
        }
    }
}
