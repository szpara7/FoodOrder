using FoodOrder.DAL;
using FoodOrder.ViewModel;
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
                products.TopRated = db.Products.OrderByDescending(o => o.Rate).Take(3).ToList();

                products.MostOrders = db.OrderLines
                    .Join(db.Products, od => od.Product.ProductID, p => p.ProductID,
                    (od, p) => new
                    {
                        Product = p,
                        OrderLine = od
                    })
                    .GroupBy(g => new {g.Product.ProductID,g.Product.Name })
                    .Select(x => new MostOrdersViewModel()
                    {
                        ProductID = x.Key.ProductID,
                        Count = x.Count(),
                        ProductName = x.Key.Name
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