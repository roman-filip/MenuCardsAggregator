using System.Net.Http;
using System.Threading.Tasks;

namespace RFI.MenuCardsAggregator.Services.Services
{
    internal class HttpService : IHttpService
    {
        #region Implementation of IHttpService

        public async Task<string> GetAsync(string uri)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
        }

        #endregion
    }
}
