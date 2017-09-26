using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodOrder.ViewModel.CRUD
{
    public class CategoryCRUDViewModel
    {
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}