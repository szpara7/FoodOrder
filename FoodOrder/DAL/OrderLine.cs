using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FoodOrder.DAL
{
    public class OrderLine
    {
        public int OrderLineID { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Value { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}