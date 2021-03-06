﻿using System;
using System.Collections.Generic;

namespace server.Models.Data
{
    public class Cart
    {
        public string Id { get; set; }
        public List<CartItem> Items { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ConfirmedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
