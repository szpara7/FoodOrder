using FoodOrder.DAL;
using FoodOrder.Interfaces.Abstract;
using FoodOrder.ViewModel.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FoodOrder.Controllers.CRUD
{
    public class ProductCRUDController : Controller
    {
        private IProductRepository productRepository;
        private IPriceRepository priceRepository;
        private IReviewRepository reviewRepository;

        public ProductCRUDController(IProductRepository productRepository, IPriceRepository priceRepository,
            IReviewRepository reviewRepository)
        {
            this.productRepository = productRepository;
            this.priceRepository = priceRepository;
            this.reviewRepository = reviewRepository;
        }

        // GET: ProductCRUD
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAll()
        {
            var model = productRepository.GetAll()
                .Join(priceRepository.GetAll(),t => t.ProductID, p => p.ProductId,
                (t,p) => new { Product = t, Price = p})
                .Where(t => t.Price.EndDate == null)
                .Select(s => new ProductCRUDViewModel
                {
                    ProductID = s.Product.ProductID,
                    Description = s.Product.Description,
                    ImageName = s.Product.ImageName,
                    IsDeleted = s.Product.IsDeleted,
                    ProductName = s.Product.ProductName,
                    Rate = s.Product.Rate,
                    Price = s.Price.Value
                })
                .ToList();

            return View(model);
        }

        public ActionResult GetById(int? productId)
        {
            if(productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var productPriceId = priceRepository.GetAll()
                .Where(t => t.ProductId == productId && t.EndDate == null)
                .Select(t => t.PriceID)
                .FirstOrDefault();

            var productPriceValue = priceRepository.GetById(productPriceId).Value;
                    

            Product product = productRepository.GetById(productId);

            ProductCRUDViewModel model = new ProductCRUDViewModel
            {
                ProductID = product.ProductID,
                Description = product.Description,
                ImageName = product.ImageName,
                IsDeleted = product.IsDeleted,
                Price = productPriceValue,
                ProductName = product.ProductName,
                Rate = product.Rate,
                CategoryId = product.CategoryId,
                PriceId = productPriceId,
                Reviews = reviewRepository.GetByProductId(productId).Select(s => new ReviewCRUDViewModel
                {
                    ReviewID = s.ReviewID,
                    AdddedDate = s.AdddedDate.ToShortDateString(),
                    Content = s.Content
                }).ToList()
            };

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int? productId)
        {
            if(productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            productRepository.Remove(productId);

            return RedirectToAction("GetAll");

        }

        [HttpPost]
        public ActionResult Edit(ProductCRUDViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("GetAll", model);
            }

            Price price = priceRepository.GetById(model.PriceId);

            
            if(price.Value != model.Price) // jesli zmiana ceny to dodanie nowej ceny
            {
                price.EndDate = DateTime.Now;
                priceRepository.Edit(price);

                priceRepository.Add(new Price
                {
                    Value = model.Price,
                    InitialDate = DateTime.Now,
                    IsDeleted = false,
                    ProductId = model.ProductID,
                    EndDate = null
                });
            }

            Product product = new Product
            {
                CategoryId = model.CategoryId,
                Description = model.Description,
                ImageName = model.ImageName,
                IsDeleted = model.IsDeleted,
                ProductID = model.ProductID,
                ProductName = model.ProductName,
                Rate = model.Rate,
            };

            productRepository.Edit(product);     

            return RedirectToAction("GetAll");
        }

    }
}