using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RFI.MenuCardsAggregator.Services.Model;

namespace RFI.MenuCardsAggregator.Services.Services
{
    public class KometaRestaurantService : BaseRestaurantService
    {
        public override string RestaurantName
        {
            get { return "Kometa Pub"; }
        }

        public KometaRestaurantService()
        {
            Uri = "http://arena.kometapub.cz/tydenni-menu.php";
        }

        public KometaRestaurantService(IHttpService httpService)
            : base(httpService)
        { }

        public override Task<MenuCard> GetMenuCardAsync()
        {
            throw new NotImplementedException();
        }
    }
}
