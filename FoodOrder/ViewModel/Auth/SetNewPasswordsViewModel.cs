using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodOrder.ViewModel.Auth
{
    public class SetNewPasswordsViewModel
    {
        public string Email { get; set; }

        [Required(ErrorMessage = "Field password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Field repeat password is requied")]
        public string Password2 { get; set; }

        public UserKind UserKind { get; set; }
    }

    public enum UserKind
    {
        Employee,
        Customer
    }
}