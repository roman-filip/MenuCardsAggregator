using System.Threading.Tasks;

namespace RFI.MenuCardsAggregator.Services.Services
{
    public interface IHttpService
    {
        Task<string> GetAsync(string uri);
    }
}
