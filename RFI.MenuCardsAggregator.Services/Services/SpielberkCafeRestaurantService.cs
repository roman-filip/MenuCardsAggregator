using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RFI.MenuCardsAggregator.Services.Model;

namespace RFI.MenuCardsAggregator.Services.Services
{
    public class SpielberkCafeRestaurantService : IRestaurantService
    {
        public SpielberkCafeRestaurantService()
        {
            Uri = @"http://www.spielberkcafe.cz/denni_menu.html";
        }


        #region Implementation of IRestaurantService

        public string RestaurantName { get; private set; }
        public string Uri { get; set; }

        public Task<MenuCard> GetMenuCardAsync()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
