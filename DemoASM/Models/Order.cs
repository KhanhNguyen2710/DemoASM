using System;
using System.Collections.Generic;

namespace DemoASM.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? UserId { get; set; }
        public double? TotalPrice { get; set; }
        public int? Quantity { get; set; }

        public virtual AspNetUser? User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
