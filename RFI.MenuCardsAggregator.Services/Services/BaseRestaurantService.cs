using System;
using System.Globalization;
using System.Threading.Tasks;
using HtmlAgilityPack;
using RFI.MenuCardsAggregator.Services.Model;

namespace RFI.MenuCardsAggregator.Services.Services
{
    public abstract class BaseRestaurantService : IRestaurantService
    {
        private static readonly CultureInfo CzCultureInfo = new CultureInfo("cs-CZ");

        private readonly IHttpService _httpService;

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
            return node.InnerText.Replace("&nbsp;", " ").Trim();
        }

        protected static decimal GetPriceFromHtmlNode(HtmlNode node)
        {
            var innerText = node.InnerText;
            var priceStr = innerText.Substring(0, innerText.IndexOf(','));
            return Convert.ToDecimal(priceStr);
        }
        
        protected static decimal GetPriceFromHtmlNodeWithKc(HtmlNode node)
        {
            var innerText = node.InnerText;
            var priceStr = innerText.Split(' ')[0];
            return Convert.ToDecimal(priceStr);
        }

        protected static DateTime CreateDate(string day, string monthName, string year)
        {
            var stringDate = string.Format("{0} {1} {2}", day, monthName, year);
            return DateTime.ParseExact(stringDate, "d MMMM yyyy", CzCultureInfo);
        }
        
        protected static DateTime CreateDate(string date)
        {
            return DateTime.ParseExact(date, "d.M.yyyy", CzCultureInfo);
        }
    }
}
