using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FoodOrder.Infrastructure
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            base.AuthorizeCore(httpContext);

            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            if (Roles == String.Empty)
            {
                return true;
            }

            var userRole = HttpContext.Current.Request.Cookies["UserRole"].Value;
            var roles = Roles.Split(',');
            if (userRole != null)
            {
                foreach (var i in roles)
                {
                    if (i.Trim() == userRole)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "NoPermission"
                };
            }
        }
    }
}