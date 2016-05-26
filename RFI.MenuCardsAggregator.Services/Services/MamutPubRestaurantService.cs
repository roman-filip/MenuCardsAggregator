using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RFI.MenuCardsAggregator.Services.Model;

namespace RFI.MenuCardsAggregator.Services.Services
{
    public class MamutPubRestaurantService : IRestaurantService
    {
        public string RestaurantName
        {
            get { return "Mamut Pub"; }
        }

        public string Uri { get; set; }

        public MamutPubRestaurantService()
        {
            Uri = "http://www.mamut-pub.cz/tydenni-menu/";
        }
        
        public Task<MenuCard> GetMenuCardAsync()
        {
            throw new NotImplementedException();
        }
    }
}
