﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrder.DAL
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required,MaxLength(15)]
        public string ProductName { get; set; }

        [Required,MaxLength(60)]
        public string Description { get; set; }

        [Range(0,5),DefaultValue(0)]
        public int Rate { get; set; }

        public string ImageName { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<OrderLine> OrderLines { get; set; }

        public virtual ICollection<Price> Prices { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}