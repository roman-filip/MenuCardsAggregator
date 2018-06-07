using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RFI.MenuCardsAggregator.Services.Model;

namespace RFI.MenuCardsAggregator.Services.Services
{
    public class UEmilaRestaurantService : BaseRestaurantService
    {
        public override string RestaurantName => "Restaurace U Emila";

        public UEmilaRestaurantService()
        {
            Uri = "https://www.restauraceuemila.cz/dennimenu/";
        }

        public UEmilaRestaurantService(IHttpService httpService)
            : base(httpService)
        { }

        public override async Task<MenuCard> GetMenuCardAsync()
        {
            var menuCard = new MenuCard(RestaurantName, Uri);
            var htmlDocument = await GetHtmlDocumentAsync();

            var dayMenu = new DayMenu { Date = DateTime.Now.Date }; //var htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("*//div[@class='date inner-layer']");

            var notFoods = new List<string> { "Bezlepkové menu", "stálé menu", "Přílohový salát", "XXL menu příplatek (1/2 menu)", "Seznam alergenů na vyžádání u obsluhy", "Jídlo zakoupené s sebou uchovejte v teplotě do 6 C spotřebovat do 2 hodin", "Gramáž masa na porci je uvedena v syrovém stavu a v minimální hmotnosti" };
            var foodDivNodes = htmlDocument.DocumentNode
                .SelectNodes("*//div[@class='left-div item-name']")
                .Where(d => !notFoods.Contains(d.InnerText.Trim()));
            foreach (var foodDivNode in foodDivNodes)
            {
                var food = new Food { Name = GetFoodName(foodDivNode.InnerText.Trim()) };
                dayMenu.Foods.Add(food);
            }

            menuCard.DayMenus.Add(dayMenu);
            return menuCard;
        }

        private static string GetFoodName(string foodName) => foodName[1] == '.' ? foodName.Substring(3) : foodName;
    }
}