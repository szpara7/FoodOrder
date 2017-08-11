using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodOrder.ViewModel.Cart
{
    public class CartViewModel
    {
        public CartViewModel()
        {
            CartLine = new List<CartLineViewModel>();
        }
        public List<CartLineViewModel> CartLine { get; set; }
        public decimal TotalValue { get; set; }
    }

    public class CartLineViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}