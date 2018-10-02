using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NLT
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");



            // routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //   defaults: new { controller = "User", action = "Test", id = UrlParameter.Optional }
            //);

            //  routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //   defaults: new { controller = "Master", action = "TestPrices", id = UrlParameter.Optional }
            //);


            // Admin
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Main", action = "Index", id = UrlParameter.Optional }
            //);


            //  // User
            //  routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //   defaults: new { controller = "User", action = "SearchTest", id = UrlParameter.Optional }
            //);

            // User
            routes.MapRoute(
              name: "Default",
              url: "{controller}/{action}/{id}",
             defaults: new { controller = "User", action = "Test", id = UrlParameter.Optional }
          );
            //    routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //   defaults: new { controller = "Reports", action = "UploadFile", id = UrlParameter.Optional }
            //);

        }
    }
}
