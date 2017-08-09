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

        public void AddProduct(Product product, int quantity,decimal price)
        {
            var cartLine = new CartLine();
         
            cartLine = lineCollection
                .Where(t => t.Product.ProductID == product.ProductID)
                .FirstOrDefault();

            if (cartLine == null)
            {
                lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity,
                    Price = price
                });
            }
            else
            {
                cartLine.Quantity += quantity;
            }
        }

        public void RemoveProduct(Product product)
        {
            lineCollection.RemoveAll(t => t.Product.ProductID == product.ProductID);
        }

        public decimal TotalValue()
        {
            return lineCollection.Sum(t => t.Quantity * t.Price);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public List<CartLine> Lines { get { return lineCollection; } }

    }

    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}