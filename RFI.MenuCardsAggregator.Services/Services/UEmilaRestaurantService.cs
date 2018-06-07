using System.Threading.Tasks;
using RFI.MenuCardsAggregator.Services.Model;

namespace RFI.MenuCardsAggregator.Services.Services
{
    public class UEmilaRestaurantService : BaseRestaurantService
    {
        public override string RestaurantName => "Restaurace U Emila";

        public UEmilaRestaurantService()
        {
            Uri = "https://www.restauraceuemila.cz/dennimenu/";
        }

        public UEmilaRestaurantService(IHttpService httpService)
            : base(httpService)
        { }

        public override Task<MenuCard> GetMenuCardAsync()
        {
            var menuCard = new MenuCard(RestaurantName, Uri);

            return Task.FromResult(menuCard);
        }
    }
}