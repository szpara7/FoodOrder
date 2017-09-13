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

            if(Roles == String.Empty || Users == String.Empty)
            {
                return true;
            }
            
            var role = HttpContext.Current.Request.Cookies["UserRole"].Value;
            if (role != null)
            {
                if (Roles == role)
                {
                    return true;
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