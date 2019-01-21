using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using RFI.MenuCardsAggregator.Services.Model;

namespace RFI.MenuCardsAggregator.Services.Services
{
    public class MamutPubRestaurantService : BaseRestaurantService
    {
        private decimal _defaultPrice;

        protected override string CurrencySymbol => ",- Kč";

        public override string RestaurantName => "Mamut Pub";

        public MamutPubRestaurantService()
        {
            Uri = "http://www.mamut-pub.cz/tydenni-menu/";
        }

        public MamutPubRestaurantService(IHttpService httpService)
            : base(httpService)
        { }

        public override async Task<MenuCard> GetMenuCardAsync()
        {
            var menuCard = new MenuCard(RestaurantName, Uri);

            var htmlDocument = await GetHtmlDocumentAsync();
            var menuStrongNode = htmlDocument.DocumentNode.QuerySelector(".detNameGalerie");
            var fridayDateHtml = menuStrongNode.InnerText.Split('-')[1];
            var date = CreateDate(fridayDateHtml).AddDays(-4);

            var defaultPriceDivNode = menuStrongNode.NextSiblingElement().GetChildElements().Where(node => node.InnerText.StartsWith("Cena menu od ")).First();
            SetDefaultPrice(defaultPriceDivNode);

            var wholeWeekDivNode = htmlDocument.DocumentNode.SelectNodes(".//div[contains(., 'Ponděl&iacute;:')]").Last().ParentNode;
            DayMenu dayMenu = null;
            foreach (var divNode in wholeWeekDivNode.GetChildElements().Where(node => node.InnerHtml != "<br>" && !string.IsNullOrWhiteSpace(node.InnerHtml)))
            {
                if (!divNode.InnerHtml.Trim().StartsWith("<span"))
                {
                    // First food for new day
                    dayMenu = new DayMenu { Date = date };
                    menuCard.DayMenus.Add(dayMenu);
                    dayMenu.Foods.Add(GetFood(divNode.ChildNodes[2]));

                    date = date.AddDays(1);
                }
                else
                {
                    var food = GetFood(divNode.ChildNodes[1]);
                    if (!string.IsNullOrEmpty(food.Name))
                    {
                        if (food.Name[0] == '(')
                        {
                            // This is not next food but additional information for previous one
                            dayMenu.Foods.Last().Name = dayMenu.Foods.Last().Name + " " + food.Name;
                        }
                        else
                        {
                            dayMenu.Foods.Add(food);
                        }
                    }
                }
            }

            return menuCard;
        }

        private void SetDefaultPrice(HtmlNode defaultPriceDivNode)
        {
            var defaultPriceText = GetStringFomHtmlNode(defaultPriceDivNode);
            var priceStartIndex = "Cena menu od ".Length;
            var priceStr = defaultPriceText.Substring(priceStartIndex, defaultPriceText.IndexOf(',') - priceStartIndex);
            _defaultPrice = Convert.ToDecimal(priceStr);
        }

        private Food GetFood(HtmlNode foodNode)
        {
            var nodeText = GetStringFomHtmlNode(foodNode).Replace("\xA0", " ");
            var food = new Food();

            var re = "(\\d+\\) )?(.*)";
            if (nodeText.EndsWith(CurrencySymbol))
            {
                re += $"( )(\\d+)({CurrencySymbol})";
            }
            var regex = new Regex(re, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var match = regex.Match(nodeText);
            if (match.Success)
            {
                food.Name = match.Groups[2].ToString();
                if (food.Name.StartsWith("pol&eacute;vka"))
                {
                    food.Price = 0;
                }
                else if (match.Groups.Count == 6)
                {
                    food.Price = Convert.ToDecimal(match.Groups[4].ToString());
                }
                else
                {
                    food.Price = _defaultPrice;
                }
            }

            return food;
        }
    }
}
