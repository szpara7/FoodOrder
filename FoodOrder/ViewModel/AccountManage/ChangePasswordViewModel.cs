using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodOrder.ViewModel.AccountManage
{
    public class ChangePasswordViewModel
    {
        public string Email { get; set; }

        [Required(ErrorMessage = "Field Old Password is required")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Field New Password is required")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Field Repeat New Password is required")]
        public string NewPassword2 { get; set; }

    }
}