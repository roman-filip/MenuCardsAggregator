using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RFI.MenuCardsAggregator.Services.Services
{
    internal class HttpService : IHttpService
    {
        #region Implementation of IHttpService

        public async Task<string> GetAsync(string uri)
        {
            using (var client = new HttpClient(new HttpClientHandler { UseProxy = false }))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("MenuCardsAggr", "1.0.0"));

                var response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
        }

        #endregion
    }
}
