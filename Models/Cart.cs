using System;
using System.Collections.Generic;

namespace ShoppingCart.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; set; }
        public Guid CartId { get; set; }
    }
}