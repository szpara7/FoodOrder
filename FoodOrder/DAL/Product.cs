using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FoodOrder.DAL
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required,MaxLength(15)]
        public string Name { get; set; }

        [Required,MaxLength(60)]
        public string Description { get; set; }

        [Range(0,5),DefaultValue(0)]
        public int Rate { get; set; }

        public string ImageName { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<OrderLine> OrderLines { get; set; }

        public virtual ICollection<Price> Prices { get; set; }
    }
}