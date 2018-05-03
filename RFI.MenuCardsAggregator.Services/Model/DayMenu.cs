using System;
using System.Collections.Generic;

namespace RFI.MenuCardsAggregator.Services.Model
{
    public class DayMenu
    {
        public DateTime Date { get; set; }

        public List<Food> Foods { get; set; }

        public DayMenu()
        {
            Foods = new List<Food>();
        }
    }
}
