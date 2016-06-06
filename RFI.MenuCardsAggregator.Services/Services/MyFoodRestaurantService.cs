using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using RFI.MenuCardsAggregator.Services.Model;

namespace RFI.MenuCardsAggregator.Services.Services
{
    public class MyFoodRestaurantService : BaseRestaurantService
    {
        public override string RestaurantName
        {
            get { return "My Food"; }
        }

        public MyFoodRestaurantService()
        {
            Uri = @"http://www.myfoodmarket.cz/brno-holandska/";
        }

        public MyFoodRestaurantService(IHttpService httpService)
            : base(httpService)
        { }

        public override Task<MenuCard> GetMenuCardAsync()
        {
            throw new NotImplementedException();
        }
    }
}
