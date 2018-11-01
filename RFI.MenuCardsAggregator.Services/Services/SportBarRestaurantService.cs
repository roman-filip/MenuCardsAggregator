using System;
using System.Threading.Tasks;
using RFI.MenuCardsAggregator.Services.Model;

namespace RFI.MenuCardsAggregator.Services.Services
{
    public class SportBarRestaurantService : BaseRestaurantService
    {
        public override string RestaurantName => "Sport Bar Arena restaurant";

        public SportBarRestaurantService()
        {
            Uri = "https://www.sportbar-arena.cz/denni-menu";
        }

        public SportBarRestaurantService(IHttpService httpService)
            : base(httpService)
        { }

        public override async Task<MenuCard> GetMenuCardAsync()
        {
            var menuCard = new MenuCard(RestaurantName, Uri)
            {
                MenuImageUri = @"https://www.sportbar-arena.cz/wp-content/uploads/2018/10/7EF49B82-CC82-4661-8347-012C8367D50D.jpeg"
            };

            return await Task.FromResult(menuCard);
        }
    }
}
