using System;
using System.Collections.Generic;

namespace server.Models.ViewModels
{
    public class CartViewModel
    {
        public string Id { get; set; }
        public List<CartItemViewModel> Items { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ConfirmedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
