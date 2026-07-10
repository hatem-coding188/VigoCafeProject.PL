using System.Collections.Generic;
using DAL.Models;

namespace BLL.Interfaces
{
    public interface ICartService
    {
        Cart GetCartByCustomerId(int customerId);
        void AddToCart(int customerId, int productId, int quantity);
        void RemoveFromCart(int cartId, int productId);
        void IncreaseQuantity(int cartId, int productId);
        void DecreaseQuantity(int cartId, int productId);
        void ClearCart(int customerId);
        decimal GetTotal(int customerId);
    }
}