using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RFI.MenuCardsAggregator.Services.Model;

namespace RFI.MenuCardsAggregator.Services.Services
{
    public class IqRestaurantService : IRestaurantService
    {
        public string RestaurantName
        {
            get { return "IQ Restaurant"; }
        }

        public string Uri { get; set; }

        public IqRestaurantService()
        {
            Uri = @"http://www.iqrestaurant.cz/brno/getData.svc?type=brnoMenuHTML";
        }

        public MenuCard GetMenuCard()
        {
            return new MenuCard(RestaurantName);
        }
    }
}
