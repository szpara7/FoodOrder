using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodOrder.ViewModel.Auth
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Field E-Mail is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Field Password is required")]
        public string Password { get; set; }

        public bool RemeberMe { get; set; }
    }
}