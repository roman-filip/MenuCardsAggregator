using System.Linq;
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
            var menuCard = new MenuCard(RestaurantName, Uri);
            var htmlDocument = await GetHtmlDocumentAsync();
            menuCard.MenuImageUri =
                htmlDocument
                    .DocumentNode
                    .SelectNodes("*//img")
                    .Last()
                    .GetAttributeValue("src", "not found");

            return menuCard;
        }
    }
}
