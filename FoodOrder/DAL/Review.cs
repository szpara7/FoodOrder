using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace FoodOrder.DAL
{
    public class Review
    {
        public int ReviewID { get; set; }

        public string Content { get; set; }

        public DateTime AdddedDate { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        [DefaultValue(false)]
        public bool IsEdited { get; set; }

        public virtual Product Products { get; set; }

        public virtual Customer Customer { get; set; }
    }
}