using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FoodOrder.DAL
{
    public class Category
    {
        public int CategoryID { get; set; }

        [Required,MaxLength(15)]
        public string CategoryName { get; set; }

        [DefaultValue(false)]
        public bool isDeleted { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}