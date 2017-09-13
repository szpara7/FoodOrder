using FoodOrder.DAL;
using FoodOrder.Infrastructure;
using FoodOrder.Interfaces;
using FoodOrder.Interfaces.Abstract;
using FoodOrder.ViewModel.AccountManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace FoodOrder.Controllers
{
    
    public class AccountManageController : Controller
    {
        private IEmployeeRepository employeeRepository;
        private ICustomerRepository customerRepository;
        private IEmailSender emailSender;

        public AccountManageController(IEmployeeRepository employeeRepository, ICustomerRepository customerRepository, IEmailSender emailSender)
        {
            this.employeeRepository = employeeRepository;
            this.customerRepository = customerRepository;
            this.emailSender = emailSender;
        }

        [CustomAuthorize]
        // GET: AccountManage
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
                if (customer != null)
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

        [CustomAuthorize]
        public ActionResult EditPersonalData()
        {
            string currentUser = HttpContext.User.Identity.Name;

            var customer = customerRepository.GetAll().Where(t => t.EMail == currentUser).FirstOrDefault();
            
            if(customer != null)
            {
                var model = new EditPersonalDataViewModel()
                {
                    UserID = customer.CustomerID,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    City = customer.City,
                    Street = customer.Street,
                    Email = customer.EMail,
                    Phone = customer.Phone,
                    PostCode = customer.PostCode
                };

                return View(model);
            }
            else
            {
                var employee = employeeRepository.GetAll().Where(t => t.Email == currentUser).FirstOrDefault();

                var model = new EditPersonalDataViewModel()
                {
                    UserID = employee.EmployeeID,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    City = employee.City,
                    Street = employee.Street,
                    Email = employee.Email,
                    Phone = employee.Phone,
                    PostCode = employee.PostCode
                };

                return View(model);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public ActionResult EditPersonalData(EditPersonalDataViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var customer = customerRepository.GetAll().Where(t => t.EMail == model.Email).FirstOrDefault();

            if(customer != null)
            {
                customer.FirstName = model.FirstName;
                customer.LastName = model.LastName;
                customer.City = model.City;
                customer.Street = model.Street;
                customer.PostCode = model.PostCode;
                customer.Phone = model.Phone;

                customerRepository.Edit(customer);
            }
            else
            {
                var employee = employeeRepository.GetAll().Where(t => t.Email == model.Email).FirstOrDefault();

                employee.FirstName = model.FirstName;
                employee.LastName = model.LastName;
                employee.City = model.City;
                employee.Street = model.Street;
                employee.PostCode = model.PostCode;
                employee.Phone = model.Phone;

                employeeRepository.Edit(employee);
            }

            TempData["PersonalData"] = "Personal data was changed";
            return View("Success");
        }
    }
}