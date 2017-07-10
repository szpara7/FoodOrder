using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FoodOrder.Infrastructure;
using FoodOrder.Models;
using FoodOrder.DAL;
using System.Collections.Generic;

namespace FoodOrder.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CanAddItemToCart()
        {

            var product = new Product()
            {
                ProductID = 1,
                ProductName = "Prod1"
            };

            var product2 = new Product()
            {
                ProductID = 2,
                ProductName = "Prod2",
                Prices = new List<Price>
                {
                    new Price()
                    {
                        PriceID = 1,
                        InitialDate = DateTime.Now,
                        Value = 30
                    }
                }
            };
            var cart = new Cart();

            cart.AddProduct(product, 1,30);
            Assert.AreEqual(1, cart.Lines.Count);

            cart.AddProduct(product, 2,30);
            Assert.AreEqual(1, cart.Lines.Count);

            cart.AddProduct(product2, 3,30);
            Assert.AreEqual(2, cart.Lines.Count);
        }

        [TestMethod]
        public void CanRemoveLineFromCart()
        {
            var product = new Product()
            {
                ProductID = 1,
                ProductName = "Prod1"
            };

            var cart = new Cart()
            {
                lineCollection = new List<CartLine>()
                {
                    new CartLine()
                    {
                        Product = product,
                        Price = 30,
                        Quantity = 4
                    }
                }
            };

            Assert.AreEqual(1, cart.Lines.Count);

            cart.RemoveProduct(product);
            Assert.AreEqual(0, cart.Lines.Count);
        }

        [TestMethod]
        public void CanReturnTotalValue()
        {
            var product = new Product()
            {
                ProductID = 1,
                ProductName = "Prod1",

            };


            var cart1 = new Cart()
            {
                lineCollection = new List<CartLine>()
                {
                    new CartLine()
                    {
                        Product = product,
                        Quantity = 4,
                        Price = 30
                    }
                }
            };

            var cart2 = new Cart()
            {
                lineCollection = new List<CartLine>()
                {
                    new CartLine()
                    {
                        Product = product,
                        Quantity = 5,
                        Price = 30
                    }
                }
            };

            var cart3 = new Cart()
            {
                
            };

            decimal value = cart1.TotalValue();
            decimal value2 = cart2.TotalValue();
            decimal value3 = cart3.TotalValue();

            Assert.AreEqual(120, value);
            Assert.AreEqual(150, value2);
            Assert.AreEqual(0, value3);
        }
    }


}
