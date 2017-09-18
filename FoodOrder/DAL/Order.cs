using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Required]
        public DeliveryMethod DeliveryMethod { get; set; }

        [DefaultValue(false)]
        public bool IsCanceled { get; set; }

        [DefaultValue(false)]
        public bool IsCompleted { get; set; }

        [DefaultValue(false)]
        public bool IsPaid { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        [ForeignKey("Employee")]
        public int? EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual ICollection<OrderLine> OrderLines { get; set; }
     
    }

    public enum PaymentMethod
    {
        [Display(Name = "Cash on delivery")]
        CashOnDelivery = 1
    }

    public enum DeliveryMethod
    {
        [Display(Name = "Home delivery")]
        HomeDelivery = 1,

        [Display(Name = "Self-pickup delivery")]
        SelfPickup
    }
}