using FoodOrder.DAL;
using FoodOrder.Interfaces.Abstract;
using FoodOrder.ViewModel.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace FoodOrder.Controllers.CRUD
{
    public class CategoryCRUDController : Controller
    {
        private ICategoryRepository categoryRepository;

        public CategoryCRUDController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        // GET: CategoryCRUD
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult GetAll()
        {
            var model = categoryRepository.GetAll()
                .Select(s => new CategoryCRUDViewModel
                {
                    CategoryId = s.CategoryID,
                    CategoryName = s.CategoryName,
                    IsDeleted = s.isDeleted
                })
                .ToList();

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Add(CategoryCRUDViewModel model)
        {
            categoryRepository.Add(new Category
            {
                CategoryName = model.CategoryName,
                isDeleted = false
            });

            return RedirectToAction("GetAll");
        }

        public ActionResult Delete(int? categoryId)
        {
            if (categoryId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!categoryRepository.Remove(categoryId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return RedirectToAction("GetAll");
        }

        [HttpPost]
        public ActionResult Edit(CategoryCRUDViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("GetAll");
            }

            Category category = new Category
            {
                CategoryID = model.CategoryId,
                CategoryName = model.CategoryName,
                isDeleted = model.IsDeleted
            };

            categoryRepository.Edit(category);

            return RedirectToAction("GetAll");

        }
    }
}