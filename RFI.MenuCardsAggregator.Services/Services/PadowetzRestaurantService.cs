﻿using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using RFI.MenuCardsAggregator.Services.Model;

namespace RFI.MenuCardsAggregator.Services.Services
{
    public class PadowetzRestaurantService : BaseRestaurantService
    {
        public override string RestaurantName => "Padowetz restaurant";

        public PadowetzRestaurantService()
        {
            Uri = "http://restaurant-padowetz.cz/poledni-menu.htm";
        }

        public PadowetzRestaurantService(IHttpService httpService)
            : base(httpService)
        { }

        public override async Task<MenuCard> GetMenuCardAsync()
        {
            var menuCard = new MenuCard(RestaurantName, Uri);
            var htmlDocument = await GetHtmlDocumentAsync();

            menuCard.DayMenus.Add(GetMenuForDay(htmlDocument, "t_pondeli"));
            menuCard.DayMenus.Add(GetMenuForDay(htmlDocument, "t_utery"));
            menuCard.DayMenus.Add(GetMenuForDay(htmlDocument, "t_streda"));
            menuCard.DayMenus.Add(GetMenuForDay(htmlDocument, "t_ctvrtek"));
            menuCard.DayMenus.Add(GetMenuForDay(htmlDocument, "t_patek"));

            return menuCard;
        }

        private DayMenu GetMenuForDay(HtmlDocument htmlDocument, string dayDivId)
        {
            var dayMenu = new DayMenu();

            var dayDiv = htmlDocument.GetElementbyId(dayDivId);
            foreach (var childElement in dayDiv.GetChildElements())
            {
                if (childElement.GetClasses().Contains("loc_datum"))
                {
                    // TODO - extract to the base class
                    // Create special class for regex constants
                    const string reMess = ".*?"; // Non-greedy match on filler
                    const string reDay = "((?:(?:[0-2]?\\d{1})|(?:[3][01]{1})))(?![\\d])";
                    const string reMonth = "((?:(?:[0]?[1-9])|(?:[1]{1}[012]{1})))(?![\\d])";
                    const string reYear = "((?:(?:[1]{1}\\d{1}\\d{1}\\d{1})|(?:[2]{1}\\d{3})))(?![\\d])";

                    var r = new Regex(reMess + reDay + reMess + reMonth + reMess + reYear, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    var m = r.Match(childElement.InnerText);
                    if (m.Success)
                    {
                        var day = m.Groups[1].ToString();
                        var month = m.Groups[2].ToString();
                        var year = m.Groups[3].ToString();

                        dayMenu.Date = CreateDate($"{day}.{month}.{year}");
                    }
                }
                else if (childElement.Id == "t_Polevky")
                {
                    foreach (var soupTr in childElement.ChildNodes)
                    {
                        dayMenu.Foods.Add(new Food { Name = soupTr.ChildNodes[1].InnerText });
                    }
                }
                else if (childElement.Id == "t_Hlavni-chod")
                {
                    foreach (var foodTr in childElement.ChildNodes)
                    {
                        var foodName = foodTr.ChildNodes[1].InnerText.Trim();
                        if (foodName != "!!!! Seznam alergenů je na vyžádání u obsluhy !!!!")
                        {
                            dayMenu.Foods.Add(new Food
                            {
                                Name = foodName,
                                Price = GetPriceFromHtmlNode(foodTr.ChildNodes[2])
                            });
                        }
                    }
                }
            }

            return dayMenu;
        }
    }
}