using FoodOrder.DAL;
using FoodOrder.Interfaces.Abstract;
using FoodOrder.ViewModel;
using FoodOrder.ViewModel.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace FoodOrder.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepository productRepository;
        private IOrderLineRepository orderLineRepository;
        private ICategoryRepository categoryRepository;

        public HomeController(IProductRepository productRepository, IOrderLineRepository orderLineRepository
            ,ICategoryRepository categoryRepository)
        {
            this.productRepository = productRepository;
            this.orderLineRepository = orderLineRepository;
            this.categoryRepository = categoryRepository;
        }

        // GET: Home
        public ActionResult Index()
        {
            var products = new HomeIndexViewModel();

            products.TopRated = productRepository.GetAll().OrderByDescending(o => o.Rate)
                .Select(x => new TopRatedViewModel()
                {
                    ProductName = x.ProductName,
                    ImageName = x.ImageName,
                    Rate = x.Rate,
                    CategoryName = x.Category.CategoryName,
                    ProductID = x.ProductID
                })
                .Take(3).ToList();
            var z = Crypto.HashPassword("12345");
            products.MostOrders = orderLineRepository.GetAll()
                .Join(productRepository.GetAll(), od => od.Product.ProductID, p => p.ProductID,
                (od, p) => new
                {
                    Product = p,
                    OrderLine = od
                })
                .GroupBy(g => new { g.Product.ProductID, g.Product.ProductName, g.Product.Category.CategoryName, g.Product.ImageName })
                .Select(x => new MostOrdersViewModel()
                {
                    ProductID = x.Key.ProductID,
                    Count = x.Count(),
                    ProductName = x.Key.ProductName,
                    CategoryName = x.Key.CategoryName,
                    ImageName = x.Key.ImageName
                })
                .OrderByDescending(x => x.Count)
                .ToList()
                .OrderBy(a => Guid.NewGuid())
                .Take(3)
                .ToList();

            if(HttpContext.User.Identity.IsAuthenticated)
            {
                string i = HttpContext.User.Identity.Name;
            }

            return View(products);
        }

        [ChildActionOnly]
        public ActionResult CategoriesList()
        {
            return PartialView("_CategoriesList", categoryRepository.GetAll());
        }
    }
}