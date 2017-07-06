using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodOrder.Infrastructure
{
    public static class UrlHelpers
    {
        public static string ProductImagePath(this UrlHelper helper, string categoryName,string productName)
        {
            string path = helper.Content("~/Images/"+"Category/" + categoryName + "/" + productName);
            return path;
            
        }
    }
}