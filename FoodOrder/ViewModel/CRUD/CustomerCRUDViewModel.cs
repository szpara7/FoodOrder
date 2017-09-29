using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodOrder.ViewModel.CRUD
{
    public class CustomerCRUDViewModel
    {
        public int CustomerID { get; set; }

        [MaxLength(20), Required]
        public string FirstName { get; set; }

        [MaxLength(30), Required]
        public string LastName { get; set; }

        [MaxLength(50), Required]
        public string City { get; set; }

        [MaxLength(50), Required]
        public string Street { get; set; }

        [Required, StringLength(6)]
        public string PostCode { get; set; }

        [Required, Phone]
        public string Phone { get; set; }

        [Required, EmailAddress]
        public string EMail { get; set; }

        [Required]
        public string CreatedDate { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsConfirmed { get; set; }
    }
}