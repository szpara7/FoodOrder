using FoodOrder.DAL;
using FoodOrder.Interfaces.Abstract;
using FoodOrder.Models;
using FoodOrder.ViewModel.Cart;
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
            return View();
        }

        public ActionResult ShowCartDataTable()
        {
            CartViewModel model = new CartViewModel();
            List<CartLine> data = GetCart().Lines;

            foreach (var i in data)
            {
                model.CartLine.Add(new CartLineViewModel()
                {
                    ProductId = i.Product.ProductID,
                    Price = i.Price,
                    ProductName = i.Product.ProductName,
                    Quantity = i.Quantity
                });
            }
            model.TotalValue = GetCart().TotalValue();

            if (!model.CartLine.Any())
            {
                ViewBag.CartEmpty = "Cart Empty!";
            }
            
            return PartialView(model);
        }

        public ActionResult ShowPartialCart()
        {
            CartViewModel model = new CartViewModel();
            List<CartLine> data = GetCart().Lines;

            foreach (var i in data)
            {
                model.CartLine.Add(new CartLineViewModel()
                {
                    ProductId = i.Product.ProductID,
                    Price = i.Price,
                    ProductName = i.Product.ProductName,
                    Quantity = i.Quantity
                });
            }
            model.TotalValue = GetCart().TotalValue();

            if (!model.CartLine.Any())
            {
                ViewBag.CartEmpty = "Cart empty!";
            }

            return PartialView(model);
        }

        public ActionResult AddToCart(int productId)
        {
            Product product = productRepository.GetById(productId);
            
            decimal price = priceRepository.GetAll()
                .Where(t => t.Product.ProductID == productId)
                .Select(x => x.Value)
                .FirstOrDefault();

            if (product != null)
            {
                GetCart().AddProduct(product, 1, price);
            }

            return RedirectToAction("ShowPartialCart", "Cart");
        }

        public ActionResult RemoveFromCart(int productId, string returnUrl = "ShowPartialCart")
        {
            Product product = productRepository.GetById(productId);

            if (product != null)
            {
                GetCart().RemoveProduct(product);
            }
            return RedirectToAction(returnUrl, "Cart");
        }

        public ActionResult ClearCart(string returnUrl)
        {
            GetCart().Clear();
            return RedirectToAction(returnUrl, "Cart");
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