using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RFI.MenuCardsAggregator.Web.Startup))]
namespace RFI.MenuCardsAggregator.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
