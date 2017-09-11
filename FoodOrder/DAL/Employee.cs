using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FoodOrder.DAL
{

    public class Employee
    {

        public int EmployeeID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string PostCode { get; set; }

        [Required,EmailAddress]
        public string Email { get; set; }

        [Required,Phone]
        public string Phone { get; set; }

        [Required]
        public string HashPassword { get; set; }

        [Required]
        public DateTime HireDate { get; set; }

        public Role Role { get; set; }

        public string Token { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        [Required]
        public decimal Salary { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
    
    public enum Role
    {
        Seller = 1,
        Cook,
        Driver
    }

}