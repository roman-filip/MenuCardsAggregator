using RFI.MenuCardsAggregator.Services.Model;
using System.Collections.Generic;

namespace RFI.MenuCardsAggregator.Web.Models
{
    public class MenuCardsViewModel
    {
        public IEnumerable<MenuCard> MenuCards { get; set; }
    }
}