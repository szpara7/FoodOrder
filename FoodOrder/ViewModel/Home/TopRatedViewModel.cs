using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodOrder.ViewModel.Home
{
    public class TopRatedViewModel
    {
        public string ProductName { get; set; }
        public int Rate { get; set; }
        public string ImageName { get; set; }
        public string CategoryName { get; set; }
        public int ProductID { get; set; }
    }
}