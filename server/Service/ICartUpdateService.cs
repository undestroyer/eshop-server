using server.Models.Data;
using server.Models.ViewModels;

namespace server.Service
{
    public interface ICartUpdateService
    {
        public Cart UpdateCart(Cart oldCart, CartViewModel newCartVM, User user);
    }
}
