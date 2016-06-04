using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using RFI.MenuCardsAggregator.Services.Model;

namespace RFI.MenuCardsAggregator.Services.Services
{
    public class SpielberkCafeRestaurantService : BaseRestaurantService
    {
        public override string RestaurantName
        {
            get { return "Spielberk café"; }
        }

        public SpielberkCafeRestaurantService()
        {
            Uri = @"http://www.spielberkcafe.cz/denni_menu.html";
        }

        public SpielberkCafeRestaurantService(IHttpService httpService)
            : base(httpService)
        { }

        public async override Task<MenuCard> GetMenuCardAsync()
        {
            var menuCard = new MenuCard(RestaurantName, Uri);

            var htmlDocument = await GetHtmlDocumentAsync();
            var date = GetMondayDate(htmlDocument);

            menuCard.DayMenus.Add(GetFoodForDay(htmlDocument, "Pondělí", date));
            date = date.AddDays(1);

            menuCard.DayMenus.Add(GetFoodForDay(htmlDocument, "Úterý", date));
            date = date.AddDays(1);

            menuCard.DayMenus.Add(GetFoodForDay(htmlDocument, "Středa", date));
            date = date.AddDays(1);

            menuCard.DayMenus.Add(GetFoodForDay(htmlDocument, "Čtvrtek", date));
            date = date.AddDays(1);

            menuCard.DayMenus.Add(GetFoodForDay(htmlDocument, "Pátek", date));

            return menuCard;
        }

        private static DateTime GetMondayDate(HtmlDocument htmlDocument)
        {
            var dailyMenuNode = htmlDocument.DocumentNode.SelectSingleNode(".//p[text()='Denní menu']");
            var dateNode = dailyMenuNode.ParentNode.NextSiblingElement().GetChildElements().First();
            var dateStr = GetStringFomHtmlNode(dateNode);
            var dateParts = dateStr.Split(' ');
            var date = CreateDate(dateParts[0] + DateTime.Now.Year);

            return date;
        }

        private DayMenu GetFoodForDay(HtmlDocument htmlDocument, string dayName, DateTime date)
        {
            var dayMenu = new DayMenu { Date = date };

            var dayNode = htmlDocument.DocumentNode.SelectSingleNode(string.Format(".//p[text()='{0}']", dayName));

            var pNode = dayNode.NextSiblingElement();
            var nodeText = GetStringFomHtmlNode(pNode);
            while (!string.IsNullOrEmpty(nodeText))
            {
                dayMenu.Foods.Add(new Food { Name = nodeText });

                pNode = pNode.NextSiblingElement();
                nodeText = GetStringFomHtmlNode(pNode);
            }

            return dayMenu;
        }
    }
}
