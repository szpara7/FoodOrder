using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodOrder.ViewModel.CRUD
{
    public class ProductCRUDViewModel
    {
        public int ProductID { get; set; }

        [Required, MaxLength(15)]
        public string ProductName { get; set; }

        [Required, MaxLength(60)]
        public string Description { get; set; }

        public int Rate { get; set; }

        public string ImageName { get; set; }

        public bool IsDeleted { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public int PriceId { get; set; }

        public List<ReviewCRUDViewModel> Reviews { get; set; }
    }
}