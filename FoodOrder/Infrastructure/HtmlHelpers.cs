using FoodOrder.Abstract;
using FoodOrder.Interfaces.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FoodOrder.Infrastructure
{
    public static class HtmlHelpers
    {
        public static bool UserIsLogged(this HtmlHelper helper)
        {
            return helper.ViewContext.HttpContext.User.Identity.IsAuthenticated;
        }

        public static bool UserHasRole(this HtmlHelper helper, params string[] roles)
        {
            if (!helper.ViewContext.HttpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            if (!roles.Any())
            {
                return true;
            }

            var userRole = HttpContext.Current.Request.Cookies["UserRole"].Value;
            if (userRole != null)
            {
                if (roles.Contains(userRole))
                {
                    return true;
                }
            }

            return false;
        }
    }
}