using server.Models.Data;
using server.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Models.Converters
{
    public class CartConverter
    {
        public static CartViewModel Convert(Cart cart)
        {
            if (cart.Items == null) throw new ArgumentNullException(nameof(cart), "У корзины должны быть заполнены позиции");
            var itemVMs = new List<CartItemViewModel>();
            foreach(var item in cart.Items)
            {
                itemVMs.Add(new CartItemViewModel 
                {
                    ProductId = item.ProductId,
                    Amount = item.Amount
                });
            }
            return new CartViewModel
            {
                Items = itemVMs,
                CreatedAt = cart.CreatedAt,
                ConfirmedAt = cart.ConfirmedAt,
                ExpiresAt = cart.ExpiresAt
            };
        }
    }
}
