using DotVVM.Framework.ViewModel;

namespace RFI.MenuCardsAggregator.Web.ViewModels
{
    public class MasterPageViewModel : DotvvmViewModelBase
    {
        public virtual string Title { get; set; }

        public MasterPageViewModel()
        {
            Title = "Agregátor jídelních lístků";
        }
    }
}
