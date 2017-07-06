using FoodOrder.DAL;
using FoodOrder.ViewModel.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodOrder.ViewModel
{
    public class HomeIndexViewModel
    {
        public IEnumerable<TopRatedViewModel> TopRated { get; set; }
        public IEnumerable<MostOrdersViewModel> MostOrders { get; set; }
    }
}