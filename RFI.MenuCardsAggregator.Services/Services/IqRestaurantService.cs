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
        private IHttpService _httpService;

        public string RestaurantName
        {
            get { return "IQ Restaurant"; }
        }

        public string Uri { get; set; }

        public IqRestaurantService()
        {
            _httpService = new HttpService();
        }

        public IqRestaurantService(IHttpService httpService)
        {
            _httpService = httpService;

            Uri = @"http://www.iqrestaurant.cz/brno/getData.svc?type=brnoMenuHTML";
        }

        public MenuCard GetMenuCard()
        {
            var pageData = _httpService.Get(Uri);

            return new MenuCard(RestaurantName);
        }
    }
}
