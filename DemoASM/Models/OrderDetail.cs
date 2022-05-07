﻿using System;
using System.Collections.Generic;

namespace DemoASM.Models
{
    public partial class OrderDetail
    {
        public int OrderId { get; set; }
        public string Isbn { get; set; } = null!;
        public int? Quantity { get; set; }
        public double? TotalPrice { get; set; }

        public virtual Book IsbnNavigation { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
