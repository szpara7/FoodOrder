using FoodOrder.Interfaces.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodOrder.Controllers
{
    public class OrderManageController : Controller
    {
       
        // GET: OrderManage
        public ActionResult OrderDetails()
        {


            return View();
        }
    }
}