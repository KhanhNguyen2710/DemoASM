using System;
using System.Collections.Generic;

namespace DemoASM.Models
{
    public partial class Cart
    {
        public string UserId { get; set; } = null!;
        public string Isbn { get; set; } = null!;
        public int? Quantity { get; set; }

        public virtual Book IsbnNavigation { get; set; } = null!;
        public virtual AspNetUser User { get; set; } = null!;
    }
}
