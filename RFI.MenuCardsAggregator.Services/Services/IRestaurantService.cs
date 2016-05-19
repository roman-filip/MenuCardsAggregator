using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RFI.MenuCardsAggregator.Services.Model;

namespace RFI.MenuCardsAggregator.Services.Services
{
    public interface IRestaurantService
    {
        string RestaurantName { get; }

        string Uri { get; set; }

        MenuCard GetMenuCard();
    }
}
