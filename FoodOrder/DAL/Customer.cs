﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FoodOrder.DAL
{
    public class Customer
    {
        public int CustomerID { get; set; }

        [MaxLength(20), Required]
        public string FirstName { get; set; }

        [MaxLength(30), Required]
        public string LastName { get; set; }

        [MaxLength(50), Required]
        public string City { get; set; }

        [MaxLength(50),Required]
        public string Street { get; set; }

        [Required,StringLength(6)]
        public string PostCode { get; set; }

        [Required,Phone]
        public string Phone { get; set; }

        [Required,EmailAddress]
        public string EMail { get; set; }

        [Required]
        public string HashPassword { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public string Token { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        [DefaultValue(false)]
        public bool IsConfirmed { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

    }

}