using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodOrder.ViewModel.Auth
{
    public class PasswordRecoveryViewModel
    {
        [Required(ErrorMessage = "Field is required")]
        public string Email { get; set; }
    }
}