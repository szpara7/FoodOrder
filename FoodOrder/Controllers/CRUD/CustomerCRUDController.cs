﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodOrder.Controllers.CRUD
{
    public class CustomerCRUDController : Controller
    {
        // GET: CustomerCRUD
        public ActionResult Index()
        {
            return PartialView();
        }
    }
}