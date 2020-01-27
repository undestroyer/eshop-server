using Microsoft.Extensions.Logging;
using server.Models;
using server.Models.Data;
using server.Models.ViewModels;
using System;
using System.Linq;

namespace server.Service
{
    public class CartUpdateService : ICartUpdateService
    {
        private readonly Context context;
        private readonly ILogger<CartUpdateService> logger;

        public CartUpdateService(Context context, ILogger<CartUpdateService> logger)
        {
            this.logger = logger;
            this.context = context;
        }

        /// <summary>
        /// Обновляет корзину пользователя новыми данными из ViewModel
        /// </summary>
        /// <param name="oldCart">Старая корзина из БД</param>
        /// <param name="newCartVM">Новая корзина из API</param>
        /// <param name="user">Пользователь к которому относится корзина</param>
        /// <returns>Экземпляр новой корзины</returns>
        public Cart UpdateCart(Cart oldCart, CartViewModel newCartVM, User user)
        {
            if (newCartVM == null) throw new ArgumentNullException(nameof(newCartVM));
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (oldCart == null)
            {
                oldCart = new Cart { UserId = user.Id };
            }
            // удаляем все товары, которые отсутствуют в новой корзине
            // обновляем количество у товаров, которые есть и в старой, и в новой корзине
            foreach (var oldItem in oldCart.Items)
            {
                if (!newCartVM.Items.Exists(x => x.ProductId == oldItem.ProductId))
                {
                    oldCart.Items.Remove(oldItem);
                }
                else
                {
                    oldItem.Amount = newCartVM.Items.First(x => x.ProductId == oldItem.ProductId).Amount;
                }
            }

            // добавляем в корзину товары, которые впервые появились в новой корзине
            foreach (var newItem in newCartVM.Items)
            {
                if (!oldCart.Items.Exists(x => x.ProductId == newItem.ProductId))
                {
                    var newCartItem = new CartItem { ProductId = newItem.ProductId, Amount = newItem.Amount };
                    oldCart.Items.Add(newCartItem);
                }
            }

            return oldCart;
        }
    }
}
