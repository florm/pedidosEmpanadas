﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BackendTp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute("Error - 404", "NotFound", new { controller = "Home", action = "Error404" });
            //routes.MapRoute(
            //    name: "DefaultApi",
            //    url: "api/{controller}/{controller}/{id}",
            //    defaults: new { controller = "home", action = "index", id = UrlParameter.Optional }
            //);
        }
    }
}
