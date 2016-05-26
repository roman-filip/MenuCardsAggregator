using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RFI.MenuCardsAggregator.Services.Model;

namespace RFI.MenuCardsAggregator.Services.Services
{
    public class KometaRestaurantService : IRestaurantService
    {
        public string RestaurantName
        {
            get { return "Kometa Pub"; }
        }

        public string Uri { get; set; }

        public KometaRestaurantService()
        {
            Uri = "http://arena.kometapub.cz/tydenni-menu.php";
        }

        public Task<MenuCard> GetMenuCardAsync()
        {
            throw new NotImplementedException();
        }
    }
}
