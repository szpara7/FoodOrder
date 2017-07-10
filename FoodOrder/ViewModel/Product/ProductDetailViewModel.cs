using FoodOrder.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodOrder.ViewModel.Product
{
    public class ProductDetailViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public int Rate { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ProductImageName { get; set; }
        public List<ReviewViewModel> Reviews { get; set; }
    }
}