using System;
using System.Threading.Tasks;
using HtmlAgilityPack;
using RFI.MenuCardsAggregator.Services.Model;

namespace RFI.MenuCardsAggregator.Services.Services
{
    public abstract class BaseRestaurantService : IRestaurantService
    {
        protected IHttpService _httpService;

        protected BaseRestaurantService()
        {
            _httpService = new HttpService();
        }

        protected BaseRestaurantService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        #region Implementation of IRestaurantService

        public virtual string Uri { get; set; }

        public abstract string RestaurantName { get; }

        public abstract Task<MenuCard> GetMenuCardAsync();

        #endregion

        protected async Task<HtmlDocument> GetHtmlDocumentAsync()
        {
            var webPage = _httpService.GetAsync(Uri);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(await webPage);
            return htmlDocument;
        }

        protected static int GetIntFomHtmlNode(HtmlNode node)
        {
            var nodeContent = node.InnerText;
            return Convert.ToInt32(nodeContent.Trim());
        }

        protected static string GetStringFomHtmlNode(HtmlNode node)
        {
            return node.InnerText.Trim();
        }
    }
}
