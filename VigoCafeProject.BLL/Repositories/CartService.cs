using System.Linq;
using BLL.Interfaces;
using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BLL.Repositories
{
    public class CartService : ICartService
    {
        private readonly AppDbContext _context;

        public CartService(AppDbContext context)
        {
            _context = context;
        }

        public Cart GetCartByCustomerId(int customerId)
            => _context.Carts
                       .Include(c => c.CartItems)
                       .ThenInclude(ci => ci.Product)
                       .FirstOrDefault(c => c.CustomerId == customerId);

        public void AddToCart(int customerId, int productId, int quantity)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.CustomerId == customerId);
            if (cart == null) return;

            var existing = _context.CartItems
                                   .FirstOrDefault(ci => ci.CartId == cart.Id && ci.ProductId == productId);

            if (existing != null)
            {
                existing.Quantity += quantity;
            }
            else
            {
                var product = _context.Products.Find(productId);
                _context.CartItems.Add(new CartItem
                {
                    CartId = cart.Id,
                    ProductId = productId,
                    Quantity = quantity,
                    UnitPrice = product.Price
                });
            }
            _context.SaveChanges();
        }

        public void RemoveFromCart(int cartItemId)
        {
            var item = _context.CartItems.Find(cartItemId);
            if (item != null)
            {
                _context.CartItems.Remove(item);
                _context.SaveChanges();
            }
        }

        public void IncreaseQuantity(int cartItemId)
        {
            var item = _context.CartItems.Find(cartItemId);
            if (item != null)
            {
                item.Quantity++;
                _context.SaveChanges();
            }
        }

        public void DecreaseQuantity(int cartItemId)
        {
            var item = _context.CartItems.Find(cartItemId);
            if (item != null)
            {
                if (item.Quantity > 1)
                    item.Quantity--;
                else
                    _context.CartItems.Remove(item);

                _context.SaveChanges();
            }
        }

        public void ClearCart(int customerId)
        {
            var cart = _context.Carts
                               .Include(c => c.CartItems)
                               .FirstOrDefault(c => c.CustomerId == customerId);
            if (cart != null)
            {
                _context.CartItems.RemoveRange(cart.CartItems);
                _context.SaveChanges();
            }
        }

        public decimal GetTotal(int customerId)
        {
            var cart = GetCartByCustomerId(customerId);
            return cart?.CartItems.Sum(i => i.UnitPrice * i.Quantity) ?? 0;
        }
    }
}