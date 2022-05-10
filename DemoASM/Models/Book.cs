using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace DemoASM.Models
{
    public partial class Book
    {
        public Book()
        {
            Carts = new HashSet<Cart>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public string Isbn { get; set; } = null!;
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Category { get; set; }
        public int? Pages { get; set; }
        public double? Price { get; set; }
        public string? Description { get; set; }
        public string? ImgUrl { get; set; }
        public int? StoreId { get; set; }

        public virtual Store? Store { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
       /* public List<SelectListItem> CategoryList { get;  set; }*/
    }
}
