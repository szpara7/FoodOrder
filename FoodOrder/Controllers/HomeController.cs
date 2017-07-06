using FoodOrder.DAL;
using FoodOrder.ViewModel;
using FoodOrder.ViewModel.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FoodOrder.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var products = new HomeIndexViewModel();
            using (var db = new DbCtx())
            {
                products.TopRated = db.Products.OrderByDescending(o => o.Rate)
                    .Select(x => new TopRatedViewModel()
                    {
                        ProductName = x.ProductName,
                        ImageName = x.ImageName,
                        Rate = x.Rate,
                        CategoryName = x.Category.CategoryName
                    })
                    .Take(3).ToList();

                products.MostOrders = db.OrderLines
                    .Join(db.Products, od => od.Product.ProductID, p => p.ProductID,
                    (od, p) => new
                    {
                        Product = p,
                        OrderLine = od
                    })
                    .GroupBy(g => new {g.Product.ProductID,g.Product.ProductName,g.Product.Category.CategoryName,g.Product.ImageName })
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

                return View(products);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [ChildActionOnly]
        public ActionResult CategoriesList()
        {
            using (var db = new DbCtx())
            {
                var categories = db.Categories.ToList();
                return PartialView("_CategoriesList", categories);
            };
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}