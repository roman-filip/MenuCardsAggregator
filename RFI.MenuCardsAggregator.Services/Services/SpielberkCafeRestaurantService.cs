using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RFI.MenuCardsAggregator.Services.Model;

namespace RFI.MenuCardsAggregator.Services.Services
{
    public class SpielberkCafeRestaurantService : BaseRestaurantService
    {
        public override string RestaurantName
        {
            get { return "Spielberk café"; }
        }

        public SpielberkCafeRestaurantService()
        {
            Uri = @"http://www.spielberkcafe.cz/denni_menu.html";
        }

        public SpielberkCafeRestaurantService(IHttpService httpService)
            : base(httpService)
        { }

        public override Task<MenuCard> GetMenuCardAsync()
        {
            throw new NotImplementedException();
        }
    }
}
