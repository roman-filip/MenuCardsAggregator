﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RFI.MenuCardsAggregator.Services.Model;

namespace RFI.MenuCardsAggregator.Services.Services
{
    public class SpielberkCafeRestaurantService : IRestaurantService
    {
        public string RestaurantName { get; private set; }

        public string Uri { get; set; }

        public SpielberkCafeRestaurantService()
        {
            Uri = @"http://www.spielberkcafe.cz/denni_menu.html";
        }

        public Task<MenuCard> GetMenuCardAsync()
        {
            throw new NotImplementedException();
        }
    }
}
