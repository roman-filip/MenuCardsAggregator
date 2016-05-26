﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RFI.MenuCardsAggregator.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "MenuCardsAggregator/{controller}/{action}/{id}",
                defaults: new { controller = "MenuCards", action = "MenuCards", id = UrlParameter.Optional }
            );
        }
    }
}
