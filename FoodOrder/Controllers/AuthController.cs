using FoodOrder.DAL;
using FoodOrder.Infrastructure;
using FoodOrder.Interfaces;
using FoodOrder.Interfaces.Abstract;
using FoodOrder.ViewModel.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
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
        private IEmailSender emailSender;

        public AuthController(IEmployeeRepository employeeRepository, ICustomerRepository customerRepository, IEmailSender emailSender)
        {
            this.employeeRepository = employeeRepository;
            this.customerRepository = customerRepository;
            this.emailSender = emailSender;
        }
        // GET: Auth

        public ActionResult Index()
        {
            string currentUser = HttpContext.User.Identity.Name ?? " ";

            return PartialView("Index", currentUser);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var isCustomerEmail = customerRepository.GetAll().Any(t => t.EMail == model.Email);
            var isEmployeeEmail = employeeRepository.GetAll().Any(t => t.Email == model.Email);

            if (!isCustomerEmail && !isEmployeeEmail)
            {
                TempData["EmailNotExist"] = "Wrong username";
                return View(model);
            }



            if (isCustomerEmail == true)
            {
                var customerPassword = customerRepository.GetAll().Where(t => t.EMail == model.Email).Select(t => t.HashPassword).FirstOrDefault();
                if (!Crypto.VerifyHashedPassword(customerPassword, model.Password))
                {
                    TempData["PasswordNotExist"] = "Wrong password";
                    return View(model);
                }

                var customerIsConfirmed = customerRepository.GetAll().Where(t => t.EMail == model.Email).Select(t => t.IsConfirmed).FirstOrDefault();
                if (customerIsConfirmed == false)
                {
                    TempData["EmailNotConfirmed"] = "E-mail wasn't confirmed!";
                    return View();
                }

                HttpCookie userRoleCookie = new HttpCookie("UserRole", "Customer");
                userRoleCookie.Expires.AddMinutes(30);
                Response.Cookies.Add(userRoleCookie);

                FormsAuthentication.SetAuthCookie(model.Email, model.RemeberMe);

            }
            else
            {
                var employeePassword = employeeRepository.GetAll().Where(t => t.Email == model.Email).Select(t => t.HashPassword).FirstOrDefault();
                if (!Crypto.VerifyHashedPassword(employeePassword, model.Password))
                {
                    TempData["PasswordNotExist"] = "Wrong password";
                    return View(model);
                }

                string role = employeeRepository.GetAll()
                    .Where(t => t.Email == model.Email).Select(t => t.Role.ToString()).FirstOrDefault();

                HttpCookie userRoleCookie = new HttpCookie("UserRole", role);
                userRoleCookie.Expires.AddMinutes(30);
                Response.Cookies.Add(userRoleCookie);

                FormsAuthentication.SetAuthCookie(model.Email, model.RemeberMe);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, ""); //usuwam cookie z przeglądarki
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);

            HttpCookie userRoleCookie = new HttpCookie("UserRole");
            userRoleCookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(userRoleCookie);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var isExistEmail = customerRepository.GetAll().Any(t => t.EMail == model.Email);
            if (isExistEmail)
            {
                TempData["EmailAlreadyExist"] = "E-mail alredy exist";
                return View(model);
            }

            if (model.Password != model.Password2)
            {
                TempData["DiffrencePasswords"] = "Passwords are diffrence";
                return View(model);
            }

            var hashPassword = Crypto.HashPassword(model.Password);
            var token = Guid.NewGuid().ToString();

            customerRepository.Add(new Customer
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                City = model.City,
                Street = model.Street,
                PostCode = model.PostCode,
                Phone = model.Phone,
                EMail = model.Email,
                HashPassword = hashPassword,
                CreatedDate = DateTime.Now,
                Token = token,
                IsDeleted = false,
                IsConfirmed = false
            });

            emailSender.SendEmail(model.Email, "Confirm password", EmailsBody.ConfirmEmail(model.FirstName, token, model.Email));

            return View("ConfirmYourEmail"); //przekierowanie do view o potwierdzeniu meila
        }

        public ActionResult ConfirmEmail(string token, string email)  //do poprawki: jeszcze pracownik!
        {
            Customer customer = customerRepository.GetAll().Where(t => t.Token == token).FirstOrDefault();
            if (customer == null)
            {
                TempData["TokenError"] = "Wrong link";
                return View();
            }

            if (customer.EMail != email)
            {
                TempData["TokenError"] = "Wrong link";
                return View();
            }

            customer.Token = Guid.NewGuid().ToString();
            customer.IsConfirmed = true;

            customerRepository.Edit(customer);

            TempData["IsConfirmed"] = "Your e-mail was confirmed.\nYou can log in.";

            return View();
        }
    }
}