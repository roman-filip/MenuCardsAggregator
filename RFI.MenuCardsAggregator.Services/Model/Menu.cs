using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFI.MenuCardsAggregator.Services.Model
{
    public class Menu
    {
        public DateTime Date { get; set; }

        public List<Food> Foods { get; set; }
    }
}
