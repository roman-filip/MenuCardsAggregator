using System;
using System.Collections.Generic;
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
            new PadowetzRestaurantService(),
            new NaTahuRestaurantService(),
            new MamutPubRestaurantService(),
            new TustoRestaurantService(),
            new UEmilaRestaurantService(),
            new KometaRestaurantService(),
            new UTrechCertuRestaurantService(),
            new SportBarRestaurantService()
            //new MyFoodRestaurantService(),
            //new IqRestaurantService()
        };

        public List<MenuCard> MenuCards { get; } = new List<MenuCard>();

        public List<MenuCard> ImageMenuCards { get; } = new List<MenuCard>();

        public List<DateTime> Days { get; } = new List<DateTime>(5);

        public DateTime SelectedDay { get; private set; }

        public MenuCardsViewModel()
        {
            FillDays();
        }

        private void FillDays()
        {
            DayOfWeek day = DateTime.Now.DayOfWeek;
            int days = day - DayOfWeek.Monday;
            DateTime monday = DateTime.Now.Date.AddDays(-days);
            Days.Add(monday);
            Days.Add(monday.AddDays(1));
            Days.Add(monday.AddDays(2));
            Days.Add(monday.AddDays(3));
            Days.Add(monday.AddDays(4));

            SelectedDay = DateTime.Now.Date;
        }

        public override async Task Load()
        {
            await base.Load();

            var resultTasks = new List<Task<MenuCard>>(_restaurantServices.Count);
            _restaurantServices.ForEach(srv => resultTasks.Add(srv.GetMenuCardAsync()));
            try
            {
                await Task.WhenAll(resultTasks);
            }
            catch
            { }

            foreach (var resultTask in resultTasks)
            {
                if (resultTask.IsCompletedSuccessfully)
                {
                    var menuCard = resultTask.Result;

                    if (menuCard.MenuImageUri == null)
                    {
                        menuCard.DayMenus.RemoveAll(d => d.Date != DateTime.Today);
                        MenuCards.Add(menuCard);
                    }
                    else
                    {
                        ImageMenuCards.Add(menuCard);
                    }
                }
                else
                {
                    // TODO - handle exception
                    MenuCards.Add(new MenuCard($"Something wrong happend for {resultTask.Exception.InnerException.TargetSite.ReflectedType.ReflectedType.Name}", string.Empty));
                }
            }
        }
    }
}
