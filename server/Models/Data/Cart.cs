using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Models.Data
{
    public class Cart
    {
        public string Id { get; set; }
        public List<CartItem> Items { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ConfirmedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
