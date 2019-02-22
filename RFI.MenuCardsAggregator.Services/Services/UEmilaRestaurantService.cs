using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using RFI.MenuCardsAggregator.Services.Model;

namespace RFI.MenuCardsAggregator.Services.Services
{
    public class UEmilaRestaurantService : BaseRestaurantService
    {
        private readonly List<string> _notFoods = new List<string>
        {
            "Bezlepkové menu :",
            "Stálé menu:",
            "Přílohový salát",
            "XXL menu příplatek (1/2 menu)",
            "Seznam alergenů na vyžádání u obsluhy",
            "Jídlo zakoupené s sebou uchovejte v teplotě do 6 °C spotřebovat do 2 hodin",
            "Gramáž masa na porci je uvedena v syrovém stavu a v minimální hmotnosti"
        };

        private MenuCard _menuCard;  // TODO - move to base class
        private DayMenu _actualDayMenu;

        public override string RestaurantName => "Restaurace U Emila";

        protected override string CurrencySymbol => " Kč";

        public UEmilaRestaurantService()
        {
            Uri = "https://www.zomato.com/cs/brno/u-emila-star%C3%A9-brno-brno-st%C5%99ed/denn%C3%AD-menu";
            InitMenuCard();
        }

        public UEmilaRestaurantService(IHttpService httpService)
            : base(httpService)
        {
            InitMenuCard();
        }

        private void InitMenuCard() => _menuCard = new MenuCard(RestaurantName, Uri);

        public override async Task<MenuCard> GetMenuCardAsync()
        {
            var htmlDocument = await GetHtmlDocumentAsync();

            var tmiGroupMtopDivs = htmlDocument.DocumentNode.SelectNodes("*//div[@class='tmi-group  mtop']");
            foreach (var tmiGroupMtopDiv in tmiGroupMtopDivs)
            {
                ProcessHtmlForOneDay(tmiGroupMtopDiv);
            }

            return _menuCard;
        }

        private void ProcessHtmlForOneDay(HtmlNode tmiGroupMtopDiv)
        {
            foreach (var node in tmiGroupMtopDiv.ChildNodes)
            {
                if (node.HasClass("tmi-group-name"))
                {
                    CreateDayMenu(node);
                }
                else if (node.HasClass("tmi-daily"))
                {
                    AddFoodIntoDayMenu(node);
                }
            }
        }

        private void CreateDayMenu(HtmlNode node)
        {
            _actualDayMenu = new DayMenu { Date = GetDate(node.FirstChild) };
            _menuCard.DayMenus.Add(_actualDayMenu);
        }

        private void AddFoodIntoDayMenu(HtmlNode node)
        {
            var foodName = GetFoodName(node.ChildNodes[1].ChildNodes[1].InnerText.Trim());
            if (!_notFoods.Contains(foodName))
            {
                var food = new Food
                {
                    Name = foodName,
                    Price = GetPriceFromHtmlNode(node.ChildNodes[3].ChildNodes[1])
                };
                _actualDayMenu.Foods.Add(food);
            }
        }

        private static DateTime GetDate(HtmlNode dateNode)
        {
            var dateParts = dateNode.InnerText.Trim().Split(' ');
            return CreateDate(dateParts[1], dateParts[2]);
        }

        private static string GetFoodName(string foodName)
        {
            var normalizedFoodName = Regex.Replace(foodName, @"\s{2,}", " ");
            return normalizedFoodName[1] == '.' ? normalizedFoodName.Substring(3) : normalizedFoodName;
        }
    }
}