using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FoodOrder.DAL
{
    public class Order
    {
        public int OrderID { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public decimal Value { get; set; }

        [Required]
        public string ShipCity { get; set; }

        [Required]
        public string ShipStreet { get; set; }

        [Required,StringLength(6)]
        public string ShipPostCode { get; set; }
        
        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        [DefaultValue(false)]
        public bool IsCanceled { get; set; }

        [DefaultValue(false)]
        public bool IsCompleted { get; set; }

        [DefaultValue(false)]
        public bool IsPaid { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual ICollection<OrderLine> OrderLines { get; set; }
     
    }

    public enum PaymentMethod
    {
        CashOnDelivery
    }
}