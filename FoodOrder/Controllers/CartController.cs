using FoodOrder.DAL;
using FoodOrder.Interfaces.Abstract;
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
        private IProductRepository productRepository;
        private IPriceRepository priceRepository;

        public CartController(IProductRepository productRepository, IPriceRepository priceRepository)
        {
            this.productRepository = productRepository;
            this.priceRepository = priceRepository;
        }
        // GET: Cart
        public ActionResult ShowCart()
        {
            return View(GetCart());
        }


        public ActionResult AddToCart(int productId)
        {
            Product product;
            decimal price;

            product = productRepository.GetAll()
                .Where(t => t.ProductID == productId)
                .FirstOrDefault();

            price = priceRepository.GetAll()
                .Where(t => t.Product.ProductID == productId)
                .Select(x => x.Value)
                .FirstOrDefault();

            if (product != null)
            {
                GetCart().AddProduct(product, 1, price);
            }

            return RedirectToAction("Index", "Home");  //todo: wracanie do tej strony
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