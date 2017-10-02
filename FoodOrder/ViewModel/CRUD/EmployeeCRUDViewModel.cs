using FoodOrder.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodOrder.ViewModel.CRUD
{
    public class EmployeeCRUDViewModel
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

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, Phone]
        public string Phone { get; set; }

        [Required]
        public string HireDate { get; set; }

        public string Role { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public decimal Salary { get; set; }

    }
}