using FoodOrder.DAL;
using FoodOrder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodOrder.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult ShowCart()
        {
            return View(GetCart());
        }


        public ActionResult AddToCart(int productId, string returnUrl = "asd")
        {
            Product product;
            decimal price;
            using (var db = new DbCtx())
            {
                product = db.Products
                    .Where(t => t.ProductID == productId)
                    .FirstOrDefault();

                price = db.Prices
                    .Where(t => t.Product.ProductID == productId)
                    .Select(x => x.Value)
                    .FirstOrDefault();
            }
            if (product != null)
            {
                GetCart().AddProduct(product, 1, price);
            }

            return RedirectToAction("Index","Home");  //todo: wracanie do tej strony
        }

        public ActionResult RemoveFromCart(int productId = 3, string returnUrl = "asd")
        {
            Product product;

            using (var db = new DbCtx())
            {
                product = db.Products
                    .Where(t => t.ProductID == productId)
                    .FirstOrDefault();
            }
            if (product != null)
            {
                GetCart().RemoveProduct(product);
            }
            
            return RedirectToAction("Index", "Home"); //todo : wracanie do strony z ktorej wywoluje sie akcje
        }

        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];

            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }

            return cart;
        }
    }
}