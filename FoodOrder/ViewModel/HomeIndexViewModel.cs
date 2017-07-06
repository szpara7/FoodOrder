using FoodOrder.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodOrder.ViewModel
{
    public class HomeIndexViewModel
    {
        public IEnumerable<Product> TopRated { get; set; }
        public IEnumerable<MostOrdersViewModel> MostOrders { get; set; }
    }
}