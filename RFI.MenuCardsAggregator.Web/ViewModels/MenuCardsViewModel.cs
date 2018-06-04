using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
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
            new MamutPubRestaurantService(),
            new MyFoodRestaurantService(),
            new NaTahuRestaurantService(),
            new PadowetzRestaurantService(),
        };

        private readonly List<MenuCard> _menuCards = new List<MenuCard>();

        public string Title { get; set; }

        public List<MenuCard> MenuCards => _menuCards;

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
                    MenuCards.Add(menuCard);
                }
                catch
                {
                    // TODO - handle exception
                    MenuCards.Add(new MenuCard($"Something wrong happend for {srv.GetType().Name}", string.Empty));
                }
            });
        }
    }
}
