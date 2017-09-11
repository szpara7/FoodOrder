using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodOrder.ViewModel.AccountManage
{
    public class EditPersonalDataViewModel
    {
        [Required]
        public int UserID { get; set; }

        [MaxLength(20), Required(ErrorMessage = "Field is required")]
        public string FirstName { get; set; }

        [MaxLength(30), Required(ErrorMessage = "Field is required")]
        public string LastName { get; set; }

        [MaxLength(50), Required(ErrorMessage = "Field is required")]
        public string City { get; set; }

        [MaxLength(50), Required(ErrorMessage = "Field is required")]
        public string Street { get; set; }

        [StringLength(6), Required(ErrorMessage = "Field is required")]
        public string PostCode { get; set; }

        [Phone, Required(ErrorMessage = "Field is required")]
        public string Phone { get; set; }

        [EmailAddress, Required(ErrorMessage = "Field is required")]
        public string Email { get; set; }
    }
}