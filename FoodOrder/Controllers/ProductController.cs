using FoodOrder.DAL;
using FoodOrder.ViewModel.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FoodOrder.Controllers
{
    public class ProductController : Controller
    {
        // GET: ProductDetails
        public ActionResult ProductsList(string categoryName)
        {
            using (var db = new DbCtx())
            {
                var result = db.Products
                    .Join(db.Prices, Product => Product.ProductID, Category => Category.Product.ProductID,
                    (t, p) => new
                    {
                        Product = t,
                        Price = p
                    })
                    .Where(t => t.Product.Category.CategoryName == categoryName
                    && t.Price.EndDate == null)
                    .Select(z => new ProductsListViewModel()
                    {
                        ProductId = z.Product.ProductID,
                        ImageName = z.Product.ImageName,
                        Description = z.Product.Description,
                        ProductName = z.Product.ProductName,
                        Price = z.Price.Value,
                        CategoryName = z.Product.Category.CategoryName               
                    }).
                    ToList();

                return View(result);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
          
        }
    }
}

//Todo: Cena w produkcie obowiązkowa

