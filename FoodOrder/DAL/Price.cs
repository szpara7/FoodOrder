using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrder.DAL
{
    public class Price
    {
        public int PriceID { get; set; }

        [Required]
        public decimal Value { get; set; }

        [Required]
        public DateTime InitialDate { get; set; }
        
        public DateTime? EndDate { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public virtual Product Product{ get; set; }
    }
}