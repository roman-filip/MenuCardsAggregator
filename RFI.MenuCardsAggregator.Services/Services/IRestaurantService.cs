using System.Threading.Tasks;
using RFI.MenuCardsAggregator.Services.Model;

namespace RFI.MenuCardsAggregator.Services.Services
{
    public interface IRestaurantService
    {
        string RestaurantName { get; }

        string Uri { get; set; }

        Task<MenuCard> GetMenuCardAsync();
    }
}
