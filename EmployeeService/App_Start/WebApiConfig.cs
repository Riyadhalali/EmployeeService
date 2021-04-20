using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace EmployeeService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //remove XML Formater and it will always return json data
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            //-> to return only xml format 
          //  config.Formatters.Remove(config.Formatters.JsonFormatter);

            // config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;  // to manage the received json data and orgainse it




        }
    }
}
