using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RFI.MenuCardsAggregator.Services.Model;

namespace RFI.MenuCardsAggregator.Services.Services
{
    public class MamutPubRestaurantService : BaseRestaurantService
    {
        public override string RestaurantName
        {
            get { return "Mamut Pub"; }
        }

        public MamutPubRestaurantService()
        {
            Uri = "http://www.mamut-pub.cz/tydenni-menu/";
        }

        public MamutPubRestaurantService(IHttpService httpService)
            : base(httpService)
        { }

        public override Task<MenuCard> GetMenuCardAsync()
        {
            throw new NotImplementedException();
        }
    }
}
