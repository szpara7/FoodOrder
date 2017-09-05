using FoodOrder.DAL;
using FoodOrder.Interfaces.Abstract;
using FoodOrder.ViewModel.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace FoodOrder.Controllers
{
    public class AuthController : Controller
    {
        private IEmployeeRepository employeeRepository;
        private ICustomerRepository customerRepository;

        public AuthController(IEmployeeRepository employeeRepository, ICustomerRepository customerRepository)
        {
            this.employeeRepository = employeeRepository;
            this.customerRepository = customerRepository;
        }
        // GET: Auth
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var pas = Crypto.HashPassword("12345");
            var isCustomerEmail = customerRepository.GetAll().Any(t => t.EMail == model.Email);
            var isEmployeeEmail = employeeRepository.GetAll().Any(t => t.Email == model.Email);

            if(!isCustomerEmail && !isEmployeeEmail)
            {
                TempData["EmailNotExist"] = "Wrong username";
                return View(model);
            }

            if(isCustomerEmail == true)
            {
                var customerPassword = customerRepository.GetAll().Where(t => t.EMail == model.Email).Select(t => t.HashPassword).FirstOrDefault(); 
                if(!Crypto.VerifyHashedPassword(customerPassword, model.Password))
                {
                    TempData["PasswordNotExist"] = "Wrong password";
                    return View(model);
                }

                FormsAuthentication.SetAuthCookie(model.Email, model.RemeberMe);
            }
            else
            {
                var employeePassword = employeeRepository.GetAll().Where(t => t.Email == model.Email).Select(t => t.HashPassword).FirstOrDefault();
                if(!Crypto.VerifyHashedPassword(employeePassword, model.Password))
                {
                    TempData["PasswordNotExist"] = "Wrong password";
                    return View(model);
                }

                FormsAuthentication.SetAuthCookie(model.Email, model.RemeberMe);
            }

            return RedirectToAction("Index", "Home");
        }
         
    }
}