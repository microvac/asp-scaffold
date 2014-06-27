using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace App.Configs
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Smartadmin Partials",
                url: "scaffold/smartadmin/partials/{*fileName}",
                defaults: new { controller = "SmartAdmin", action = "Partials", fileName = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Smartadmin",
                url: "scaffold/smartadmin/{*fileName}",
                defaults: new { controller = "SmartAdmin", action = "Index", fileName = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "App", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}