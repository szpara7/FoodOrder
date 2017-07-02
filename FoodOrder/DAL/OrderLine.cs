using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FoodOrder.DAL
{
    public class OrderLine
    {
        public int OrderLineID { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Value { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}