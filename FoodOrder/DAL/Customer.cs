using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FoodOrder.DAL
{
    public class Customer
    {
        public int CUstomerID { get; set; }

        [MaxLength(15), Required]
        public string FirstName { get; set; }

        [MaxLength(15), Required]
        public string LastName { get; set; }

        [MaxLength(25), Required]
        public string City { get; set; }

        [MaxLength(15),Required]
        public string Street { get; set; }

        [Required,StringLength(6)]
        public string PostCode { get; set; }

        [Required,Phone]
        public string Phone { get; set; }

        [Required,EmailAddress]
        public string EMail { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

    }

}