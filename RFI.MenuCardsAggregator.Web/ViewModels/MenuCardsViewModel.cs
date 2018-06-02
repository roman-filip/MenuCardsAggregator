using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;

namespace RFI.MenuCardsAggregator.Web.ViewModels
{
    public class MenuCardsViewModel : MasterPageViewModel
    {
        public string Title { get; set; }

        public MenuCardsViewModel()
        {
            Title = "Jidelni listky";
        }
    }
}
