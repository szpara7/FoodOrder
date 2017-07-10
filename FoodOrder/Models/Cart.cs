using FoodOrder.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodOrder.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddProduct(Product product,int quantity)
        {
            var cartLine = new CartLine();

            cartLine = lineCollection
                .Where(t => t.Product.ProductID == product.ProductID)
                .FirstOrDefault();

            if(cartLine == null)
            {
                lineCollection.Add(new CartLine { Product = product, Quantity = quantity});
            }
            else
            {
                cartLine.Quantity += quantity;
            }
        }

        public void RemoveLine(Product product)
        {
            lineCollection.RemoveAll(t => t.Product.ProductID == product.ProductID);
        }

        public decimal TotalValue()
        {
            return lineCollection.Sum(t => t.Product.Prices
            .Where(l => l.Product.ProductID == t.Product.ProductID && l.EndDate == null)
            .Select(x => x.Value).FirstOrDefault() * t.Quantity);      
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines { get { return lineCollection; } }
    }



    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}