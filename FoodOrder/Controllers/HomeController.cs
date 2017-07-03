using FoodOrder.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodOrder.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();

        }


        [ChildActionOnly]
        public ActionResult CategoriesList()
        {
            using (var db = new DbCtx())
            {
                var categories = db.Categories.ToList();
                return PartialView("_CategoriesList", categories);
            };
            
        }
    }
}