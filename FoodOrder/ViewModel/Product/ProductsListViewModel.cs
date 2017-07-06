using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodOrder.ViewModel.Product
{
    public class ProductsListViewModel
    {
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public string  ImageName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

    }
}