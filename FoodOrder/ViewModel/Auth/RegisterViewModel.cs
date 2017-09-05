using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodOrder.ViewModel.Auth
{
    public class RegisterViewModel
    {
        [MaxLength(20), Required(ErrorMessage = "Field First name is required")]
        public string FirstName { get; set; }

        [MaxLength(30), Required(ErrorMessage = "Field Last name is required")]
        public string LastName { get; set; }

        [MaxLength(50), Required(ErrorMessage = "Field City is required")]
        public string City { get; set; }

        [MaxLength(50), Required(ErrorMessage = "Field Street is required")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Field Post code is required"), StringLength(6),DataType(DataType.PostalCode), RegularExpression("[0-9]{2}\\-[0-9]{3}",ErrorMessage = "Format: 00-000")]
        public string PostCode { get; set; }

        [Required(ErrorMessage = "Field Phone is required"), StringLength(9)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Field Email is required"), EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Field Password is required"), DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Field Password is required"), DataType(DataType.Password)]
        public string Password2 { get; set; }
    }
}