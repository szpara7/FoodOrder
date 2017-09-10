using FoodOrder.DAL;
using FoodOrder.Infrastructure;
using FoodOrder.Interfaces;
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

                FormsAuthentication.SetAuthCookie(model.Email, model.RemeberMe);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);

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

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            model.Email = HttpContext.User.Identity.Name;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (employeeRepository.GetAll().Any(t => t.Email == model.Email))
            {
                Employee employee = employeeRepository.GetAll().Where(t => t.Email == model.Email).FirstOrDefault();
                if (!Crypto.VerifyHashedPassword(employee.HashPassword, model.OldPassword))
                {
                    TempData["WrongOldPassword"] = "Old password is wrong";
                    return View(model);
                }

                if (model.NewPassword != model.NewPassword2)
                {
                    TempData["NewPasswordsDiffrence"] = "The new passwords are diffrence";
                    return View(model);
                }

                employee.HashPassword = Crypto.HashPassword(model.NewPassword);

                employeeRepository.Edit(employee);
            }
            else
            {
                Customer customer = customerRepository.GetAll().Where(t => t.EMail == model.Email).FirstOrDefault();
                if (!Crypto.VerifyHashedPassword(customer.HashPassword, model.OldPassword))
                {
                    TempData["WrongOldPassword"] = "Old password is wrong";
                    return View(model);
                }

                if (model.NewPassword != model.NewPassword2)
                {
                    TempData["NewPasswordsDiffrence"] = "The new passwords are diffrence";
                    return View(model);
                }

                customer.HashPassword = Crypto.HashPassword(model.NewPassword);

                customerRepository.Edit(customer);
            }

            return View("PasswordChanged");
        }

        public ActionResult PasswordRecovery()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PasswordRecovery(PasswordRecoveryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!employeeRepository.GetAll().Any(t => t.Email == model.Email)
                && !customerRepository.GetAll().Any(t => t.EMail == model.Email))
            {
                TempData["EmailNotExist"] = "This e-mail doesn't exist";
                return View(model);
            }
            else
            {
                Customer customer = customerRepository.GetAll().Where(t => t.EMail == model.Email).FirstOrDefault();
                if(customer != null)
                {
                    var userName = customer.FirstName;
                    var userToken = customer.Token;
                    var userEmail = customer.EMail;

                    emailSender.SendEmail(userEmail, "Password recovery", EmailsBody.RecoverPassword(
                        userName, userToken, userEmail));

                    return View("PasswordRecoveryStatement");
                }
                else
                {
                    Employee employee = employeeRepository.GetAll().Where(t => t.Email == model.Email).FirstOrDefault();

                    var userName = employee.FirstName;
                    var userToken = employee.Token;
                    var userEmail = employee.Email;

                    emailSender.SendEmail(userEmail, "Password recovery", EmailsBody.RecoverPassword(
                        userName, userToken, userEmail));

                    return View("PasswordRecoveryStatement");
                } 
            }
        }

        public ActionResult SetNewPassword(string token, string email)
        {
            SetNewPasswordsViewModel model = new SetNewPasswordsViewModel()
            {
                Email = email
            };

            Customer customer = customerRepository.GetAll().Where(t => t.EMail == email).FirstOrDefault();
            Employee employee = employeeRepository.GetAll().Where(t => t.Email == email).FirstOrDefault();


            if (customer != null)
            {
                if (customer.Token == token)
                {
                    model.UserKind = UserKind.Customer;
                    return View(model);
                }
                else
                {
                    TempData["TokenError"] = "Wrong link";
                    return View("Error");
                }
            }
            else if (employee != null)
            {
                if (employee.Token == token)
                {
                    model.UserKind = UserKind.Employee;
                    return View(model);
                }
                else
                {
                    TempData["TokenError"] = "Wrong link";
                    return View("Error");
                }
            }

            TempData["TokenError"] = "Wrong link";

            return View("Error");
        }

        [HttpPost]
        public ActionResult SetNewPassword(SetNewPasswordsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Password != model.Password2)
            {
                TempData["DiffrencePasswords"] = "Passwords are diffrence";
                return View(model);
            }

            if (model.UserKind == UserKind.Customer)
            {
                Customer customer = customerRepository.GetAll().Where(t => t.EMail == model.Email).FirstOrDefault();
                customer.HashPassword = Crypto.HashPassword(model.Password);
                customer.Token = Guid.NewGuid().ToString();

                customerRepository.Edit(customer);
            }
            else if (model.UserKind == UserKind.Employee)
            {
                Employee employee = employeeRepository.GetAll().Where(t => t.Email == model.Email).FirstOrDefault();
                employee.HashPassword = Crypto.HashPassword(model.Password);
                employee.Token = Guid.NewGuid().ToString();

                employeeRepository.Edit(employee);
            }

            TempData["SetPasswordSuccess"] = "Your password was changed.\nNow you can log in.";
            return View("Success");
        }
    }
}