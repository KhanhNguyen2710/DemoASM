using System;
using System.Collections.Generic;

namespace DemoASM.Models
{
    public partial class Store
    {
        public Store()
        {
            Books = new HashSet<Book>();
        }

        public int StoreId { get; set; }
        public string? Name { get; set; }
        public string? UserId { get; set; }
        public string? Phone { get; set; }

        public virtual AspNetUser? User { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
