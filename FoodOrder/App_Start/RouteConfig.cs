using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FoodOrder
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              name: "ProductsList",
              url: "Categories/{categoryName}",
              defaults: new { controller = "Product", action = "ProductsList" }
              );


            routes.MapRoute(
               name: "ProductDetails",
                url: "product-{productId}",
                defaults: new { controller = "Product", action = "ProductDetails" }
               );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

                   
        }
    }
}
