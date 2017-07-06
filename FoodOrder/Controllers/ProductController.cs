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
        public ActionResult ProductsList(int categoryId = 1)
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
                    .Where(t => t.Product.Category.CategoryID == categoryId)
                    .Select(z => new ProductsListViewModel()
                    {
                        ProductId = z.Product.ProductID,
                        ImageName = z.Product.ImageName,
                        Description = z.Product.Description,
                        ProductName = z.Product.ProductName,
                        Price = z.Price.Value                  
                    }).
                    ToList();

                return View(result);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
          
        }
    }
}