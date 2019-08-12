using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Vidly_Web_Application
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var settings = config.Formatters.JsonFormatter.SerializerSettings;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            settings.Formatting = Formatting.Indented;
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }

            );

            //config.Routes.Add("confirmation",
            //    new Route("Users/Edit/{email}",
            //    new RouteValueDictionary(new { controller = "Users", action = "Edit", email = UrlParameter.Optional }
            //    new MvcRouteHandler())
            //    );
        }
    }
}
