using FoodOrder.Abstract;
using FoodOrder.Interfaces.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if(!helper.ViewContext.HttpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            var currentUser = HttpContext.Current.User.Identity.Name;
            EmployeeRepository employeeRepository = new EmployeeRepository();
            var userRole = employeeRepository.GetAll().Where(t => t.Email == currentUser).Select(t => t.Role.ToString()).FirstOrDefault();

            if(roles.Contains(userRole))
            {
                return true;
            }
            return false;
        }
   
    }
}