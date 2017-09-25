using FoodOrder.DAL;
using FoodOrder.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodOrder.ViewModel.OrderManage
{
    public class OrderDetailsViewModel
    {
       
        [Required(ErrorMessage = "This field is required",AllowEmptyStrings = false)]
        public DeliveryMethod DeliveryMethod { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public PaymentMethod PaymentMethod { get; set; }

        public int CustomerId { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string CustomerFirstName{ get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string CustomerLastName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string CustomerEmail { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string ShipCity { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string ShipStreet { get; set; }

        [Required(ErrorMessage = "This field is required"), RegularExpression("[0-9]{2}\\-[0-9]{3}")]
        public string PostCode { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public decimal Value { get; set; }

        [Required(ErrorMessage = "This field is required"), Phone]
        public string Phone { get; set; }

        [Required]
        public List<OrderLineViewModel> CartLines { get; set; }
    }
}