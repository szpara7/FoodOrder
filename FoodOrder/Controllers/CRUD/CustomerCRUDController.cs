using FoodOrder.DAL;
using FoodOrder.Infrastructure;
using FoodOrder.Interfaces;
using FoodOrder.Interfaces.Abstract;
using FoodOrder.ViewModel.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FoodOrder.Controllers.CRUD
{
    public class CustomerCRUDController : Controller
    {
        private ICustomerRepository customerRepository;
        private IEmailSender emailSender;

        public CustomerCRUDController(ICustomerRepository customerRepository, IEmailSender emailSender)
        {
            this.customerRepository = customerRepository;
            this.emailSender = emailSender;
        }

        // GET: CustomerCRUD
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult GetAll()
        {
            var model = customerRepository.GetAll()
                .Select(s => new CustomerCRUDViewModel
                {
                    CustomerID = s.CustomerID,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    EMail = s.EMail
                })
                .ToList();

            return PartialView(model);
        }

        public ActionResult GetById(int? customerId)
        {
            if (customerId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
            }

            Customer customer = customerRepository.GetById(customerId);

            CustomerCRUDViewModel model = new CustomerCRUDViewModel
            {
                CustomerID = customer.CustomerID,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                City = customer.City,
                CreatedDate = customer.CreatedDate.ToLongDateString(),
                EMail = customer.EMail,
                IsConfirmed = customer.IsConfirmed,
                IsDeleted = customer.IsDeleted,
                Phone = customer.Phone,
                PostCode = customer.PostCode,
                Street = customer.Street
            };

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public void SendEmailConfirmation(string userEmail)
        {
            string userToken = customerRepository.GetByEmail(userEmail).Token;
            string userName = customerRepository.GetByEmail(userEmail).FirstName;

            try
            {
                emailSender.SendEmail(userEmail, "Confirm email", EmailsBody.ConfirmEmail(userName, userToken, userEmail));
            }
            catch(Exception e) {  }

        }
    }
}