using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using RFI.MenuCardsAggregator.Services.Model;

namespace RFI.MenuCardsAggregator.Services.Services
{
    public abstract class BaseRestaurantService : IRestaurantService
    {
        private static readonly CultureInfo CzCultureInfo = new CultureInfo("cs-CZ");

        private readonly IHttpService _httpService;

        private Regex _regexPrice;

        private Regex RegexPrice => _regexPrice ?? (_regexPrice = new Regex($".*?(\\d+)({CurrencySymbol})", RegexOptions.IgnoreCase | RegexOptions.Singleline));

        protected virtual string CurrencySymbol => string.Empty;

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

        // TODO - use this method instead of node.InnerText in concrete services
        protected static int GetIntFomHtmlNode(HtmlNode node)
        {
            var nodeContent = node.InnerText;
            return Convert.ToInt32(nodeContent.Trim());
        }

        protected static string GetStringFomHtmlNode(HtmlNode node)
        {
            return node.InnerText.Replace("&nbsp;", " ").Trim();
        }

        protected decimal GetPriceFromHtmlNode(HtmlNode node)
        {
            var innerText = GetStringFomHtmlNode(node);
            Match match = RegexPrice.Match(innerText);
            if (match.Success)
            {
                var priceStr = match.Groups[1].ToString();
                return Convert.ToDecimal(priceStr);
            }

            return 0;
        }

        protected static DateTime CreateDate(string day, string monthName, string year)
        {
            var stringDate = $"{day} {monthName} {year}";
            return DateTime.ParseExact(stringDate, "d MMMM yyyy", CzCultureInfo);
        }

        protected static DateTime CreateDate(string day, string monthName)
        {
            var stringDate = $"{day} {monthName}";
            return DateTime.ParseExact(stringDate, "d MMMM", CzCultureInfo);
        }

        protected static DateTime CreateDate(string date)
        {
            return DateTime.ParseExact(date.Trim(), "d.M.yyyy", CzCultureInfo);
        }
    }
}
