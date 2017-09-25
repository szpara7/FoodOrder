using FoodOrder.DAL;
using FoodOrder.Infrastructure;
using FoodOrder.Interfaces;
using FoodOrder.Interfaces.Abstract;
using FoodOrder.Models;
using FoodOrder.ViewModel.OrderManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FoodOrder.Controllers
{
    public class OrderManageController : Controller
    {
        private ICustomerRepository customerRepository;
        private IOrderRepository orderRepository;
        private IOrderLineRepository orderLineRepository;
        private IEmailSender emailSender;

        public OrderManageController(ICustomerRepository customerRepository, IOrderRepository orderRepository,
            IOrderLineRepository orderLineRepository, IEmailSender emailSender)
        {
            this.customerRepository = customerRepository;
            this.orderRepository = orderRepository;
            this.orderLineRepository = orderLineRepository;
            this.emailSender = emailSender;
        }


        [CustomAuthorize(Roles = "Customer")]
        public ActionResult OrderDetails()
        {
            string currentUserName = HttpContext.User.Identity.Name;
          
            Customer customer = customerRepository.GetByEmail(currentUserName);

            if (customer == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            decimal cartValue = cart.TotalValue();

            List<OrderLineViewModel> cartLines = new List<OrderLineViewModel>();
            foreach (var i in cart.Lines)
            {
                cartLines.Add(new OrderLineViewModel
                {
                    ProductId = i.Product.ProductID,
                    Quantity = i.Quantity,
                    Price = i.Price,
                    ProductName = i.Product.ProductName
                });
            }

            var model = new OrderDetailsViewModel()
            {
                CustomerId = customer.CustomerID,
                CustomerFirstName = customer.FirstName,
                CustomerLastName = customer.LastName,
                ShipCity = customer.City,
                ShipStreet = customer.Street,
                PostCode = customer.PostCode,
                Phone = customer.Phone,
                Value = cartValue,
                CartLines = cartLines,
                CustomerEmail = customer.EMail
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult OrderDetails(OrderDetailsViewModel model)
        {
            int orderId;

            if (model.DeliveryMethod != DeliveryMethod.SelfPickup)
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var order = new Order
                {
                    CustomerId = model.CustomerId,
                    DeliveryMethod = model.DeliveryMethod,
                    OrderDate = DateTime.Now,
                    PaymentMethod = model.PaymentMethod,
                    ShipCity = model.ShipCity,
                    ShipStreet = model.ShipStreet,
                    ShipPostCode = model.PostCode,
                    Value = model.Value
                };

                orderRepository.Add(order);

                orderId = order.OrderID;
            }
            else
            {
                var order = new Order
                {
                    CustomerId = model.CustomerId,
                    DeliveryMethod = model.DeliveryMethod,
                    OrderDate = DateTime.Now,
                    PaymentMethod = model.PaymentMethod,
                    ShipCity = "-",
                    ShipStreet = "-",
                    ShipPostCode = "-",
                    Value = model.Value
                };

                orderRepository.Add(order);

                orderId = order.OrderID;
            }

            foreach (var i in model.CartLines)
            {
                orderLineRepository.Add(new OrderLine
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    OrderId = orderId,
                    LineValue = i.Price * i.Quantity,
                    UnitPrice = i.Price
                });
            }

            emailSender.SendEmail(model.CustomerEmail, "Order",
                EmailsBody.SubmitOrder(model.CustomerFirstName, model.CustomerEmail, model.Value));            

            TempData["AddOrderSuccess"] = "Your order is being processed";
            return View("Success");

        }

        [CustomAuthorize(Roles = "Customer")]
        public ActionResult UserOrders()
        {
            string currentUserName = HttpContext.User.Identity.Name;
            
            int customerId = customerRepository.GetByEmail(currentUserName).CustomerID;

            var model = orderRepository.GetAll()
                .Where(t => t.CustomerId == customerId)
                .OrderByDescending(t => t.OrderDate)
                .Select(s => new UserOrdersViewModel
                {
                    OrderDate = s.OrderDate,
                    OrderValue = s.Value,
                    OrderLines = s.OrderLines
                    .Select(t => new OrderLineViewModel
                    {
                        Price = t.UnitPrice,
                        ProductId = t.ProductId,
                        ProductName = t.Product.ProductName,
                        Quantity = t.Quantity
                    })
                    .ToList()
                })
                .ToList();            

            return View(model);
        }
    }
}