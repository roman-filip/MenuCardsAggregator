using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace RFI.MenuCardsAggregator.Services.Services
{
    internal class HttpService : IHttpService
    {
        #region Implementation of IHttpService

        public async Task<string> GetAsync(string uri)
        {
            var config = new HttpClientHandler
            {
                UseProxy = true,
                Proxy = new MyProxy("http://proxy.homecredit.cz:81")
            };

            using (var client = new HttpClient(/*config*/))
            {
                var response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
        }

        #endregion
    }

    public class MyProxy : IWebProxy
    {
        public MyProxy(string proxyUri)
            : this(new Uri(proxyUri))
        {
        }

        public MyProxy(Uri proxyUri)
        {
            this.ProxyUri = proxyUri;
        }

        public Uri ProxyUri { get; set; }

        public ICredentials Credentials
        {
            get => null; // new NetworkCredential(@"", "");
            set { }
        }

        public Uri GetProxy(Uri destination)
        {
            return this.ProxyUri;
        }

        public bool IsBypassed(Uri host)
        {
            return false; /* Proxy all requests */
        }
    }
}
