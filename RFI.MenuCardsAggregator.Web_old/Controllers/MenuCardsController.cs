using RFI.MenuCardsAggregator.Services.Model;
using RFI.MenuCardsAggregator.Services.Services;
using RFI.MenuCardsAggregator.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RFI.MenuCardsAggregator.Web.Controllers
{
    public class MenuCardsController : Controller
    {
        // GET: MenuCards
        public async Task<ActionResult> MenuCards(MenuCardsViewModel model)
        {
            model.MenuCards = await GetAllMenuCardsAsync();

            return View(model);
        }

        private static async Task<IEnumerable<MenuCard>> GetAllMenuCardsAsync()
        {
            var restaurantServices = new IRestaurantService[]
            {
                new IqRestaurantService(),
                new MyFoodRestaurantService(),
                new KometaRestaurantService(),
                new MamutPubRestaurantService(),
                new NaTahuRestaurantService()
            };
            var tasks = new List<Task<MenuCard>>();

            restaurantServices.ToList().ForEach(service => tasks.Add(service.GetMenuCardAsync()));
            await Task.WhenAll(tasks);

            var menuCards = new List<MenuCard>(tasks.Count);
            tasks.ForEach(task =>
            {
                task.Result.DayMenus.RemoveAll(dayMenu => dayMenu.Date.Date != DateTime.Now.Date);
                menuCards.Add(task.Result);
            });

            return menuCards;
        }
    }
}
