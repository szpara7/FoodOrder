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

        public ActionResult ProductDetails(int productId = 1)
        {
            //todo: change layout
            using (var db = new DbCtx())
            {
                var result = db.Products
                    .Join(db.Prices, p => p.ProductID, t => t.Product.ProductID,
                    (p, t) => new
                    {
                        Product = p,
                        Price = t
                    })
                    .Where(p => p.Product.ProductID == productId
                    && p.Price.EndDate == null)
                    .Select(x => new ProductDetailViewModel()
                    {
                        ProductName = x.Product.ProductName,
                        CategoryName = x.Product.Category.CategoryName,
                        Description = x.Product.Description,
                        Rate = x.Product.Rate,
                        Price = x.Price.Value,
                        Reviews = x.Product
                        .Reviews
                        .Select(k => new ReviewViewModel()
                        {
                            AddedDate = k.AdddedDate,
                            Content = k.Content,
                            CustomerFirstName = k.Customer.FirstName,
                            CustomerLastName = k.Customer.LastName
                        })
                        .ToList(),
                        ProductImageName = x.Product.ImageName,
                        ProductId = x.Product.ProductID
                    })
                    .FirstOrDefault();

                var cos = db.Prices.Where(t => t.Product.ProductID == 3 && t.EndDate == null)
                    .Select(x => x.Value).FirstOrDefault();

                return View(result);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                
        }
    }
}

//Todo: Cena w produkcie obowiązkowa

