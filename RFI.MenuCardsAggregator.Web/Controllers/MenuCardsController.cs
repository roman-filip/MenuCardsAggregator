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
            IRestaurantService[] restaurantServices = new[] { new IqRestaurantService() };
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






        private static IEnumerable<MenuCard> GetMenuCardsMock()
        {
            return new List<MenuCard>
            {
                new MenuCard("Kometa")
                {
                    DayMenus = new List<DayMenu>
                    {
                        new DayMenu { Date = DateTime.Now, Foods = new List<Food> { new Food { Name = "Food 1" }, new Food { Name = "Food 2"} } },
                        new DayMenu { Date = DateTime.Now.AddDays(1), Foods = new List<Food> { new Food { Name = "Food 3" }, new Food { Name = "Food 4"} } }}
                    },
                    new MenuCard("IQ") { DayMenus = new List<DayMenu>
                    {
                        new DayMenu { Date = DateTime.Now, Foods = new List<Food> { new Food { Name = "Food 10" }, new Food { Name = "Food 20"} } },
                        new DayMenu { Date = DateTime.Now.AddDays(1), Foods = new List<Food> { new Food { Name = "Food 30" }, new Food { Name = "Food 40"} } },
                        new DayMenu { Date = DateTime.Now.AddDays(2), Foods = new List<Food> { new Food { Name = "Food 300" } } }
                    }
                }
            };
        }
    }
}
