using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodOrder.ViewModel.Product
{
    public class ReviewViewModel
    {
        public string Content { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public DateTime AddedDate { get; set; }
    }
}