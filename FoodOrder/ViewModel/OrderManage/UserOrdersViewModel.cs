using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodOrder.ViewModel.OrderManage
{
    public class UserOrdersViewModel
    {
        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public decimal OrderValue { get; set; }

        [Required]
        public IEnumerable<OrderLineViewModel> OrderLines { get; set; }

    }


}