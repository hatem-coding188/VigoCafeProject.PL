using DAL.Models;

namespace BLL.Interfaces
{
    public interface ICartService
    {
        Cart GetCartByCustomerId(int customerId);
        void AddToCart(int customerId, int productId, int quantity);
        void RemoveFromCart(int cartItemId);
        void IncreaseQuantity(int cartItemId);
        void DecreaseQuantity(int cartItemId);
        void ClearCart(int customerId);
        decimal GetTotal(int customerId);
    }
}