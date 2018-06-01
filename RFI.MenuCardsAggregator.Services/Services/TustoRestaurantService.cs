using System.Threading.Tasks;
using RFI.MenuCardsAggregator.Services.Model;

namespace RFI.MenuCardsAggregator.Services.Services
{
    public class TustoRestaurantService : BaseRestaurantService
    {
        public override string RestaurantName => "Tusto restaurant";

        public TustoRestaurantService()
        {
            Uri = "http://titanium.tusto.cz/tydenni-menu/";
        }

        public TustoRestaurantService(IHttpService httpService)
            : base(httpService)
        { }

        public override Task<MenuCard> GetMenuCardAsync()
        {
            return Task.FromResult(new MenuCard(RestaurantName, Uri));
        }
    }
}