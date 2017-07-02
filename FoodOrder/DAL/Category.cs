using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FoodOrder.DAL
{
    public class Category
    {
        public int CategoryID { get; set; }

        [Required,MaxLength(15)]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}