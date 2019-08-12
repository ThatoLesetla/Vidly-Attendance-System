using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Vidly_Web_Application
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Enables http requests to send posts when they identify email addresses
            routes.MapRoute(
                "confirmation",
                "Users/Edit/{email}",
                new { controller = "Users", action = "Edit", email = UrlParameter.Optional }
                );

            routes.MapRoute(
                "Deleteconfirmation",
                "Users/Delete/{email}",
                new { controller = "Users", action = "Delete", email = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}
