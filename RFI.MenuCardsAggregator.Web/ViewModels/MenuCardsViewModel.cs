using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RFI.MenuCardsAggregator.Services.Model;
using RFI.MenuCardsAggregator.Services.Services;

namespace RFI.MenuCardsAggregator.Web.ViewModels
{
    public class MenuCardsViewModel : MasterPageViewModel
    {
        // TODO - use IoC container
        private readonly List<IRestaurantService> _restaurantServices = new List<IRestaurantService>()
        {
            new IqRestaurantService(),
            new KometaRestaurantService(),
            //new MamutPubRestaurantService(),
            //new MyFoodRestaurantService(),
            new NaTahuRestaurantService(),
            new PadowetzRestaurantService(),
            new TustoRestaurantService(),
            //new UEmilaRestaurantService()
        };

        public string Title { get; set; }

        public List<MenuCard> MenuCards { get; } = new List<MenuCard>();

        public MenuCardsViewModel()
        {
            Title = "Jidelni listky";
        }

        public override async Task Load()
        {
            await base.Load();

            // TODO - run it in parallel
            _restaurantServices.ForEach(srv =>
            {
                try
                {
                    var menuCard = srv.GetMenuCardAsync().Result;
                    menuCard.DayMenus.RemoveAll(d => d.Date != DateTime.Today);
                    MenuCards.Add(menuCard);
                }
                catch (Exception)
                {
                    // TODO - handle exception
                    MenuCards.Add(new MenuCard($"Something wrong happend for {srv.GetType().Name}", string.Empty));
                }
            });
        }
    }
}
