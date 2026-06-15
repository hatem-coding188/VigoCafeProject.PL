using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces;
using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BLL.Repositories
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetAll()
            => _context.Orders
                       .Include(o => o.Customer)
                       .Include(o => o.OrderDetails)
                       .ThenInclude(od => od.Product)
                       .ToList();

        public IEnumerable<Order> GetByCustomerId(int customerId)
            => _context.Orders
                       .Where(o => o.CustomerId == customerId)
                       .Include(o => o.OrderDetails)
                       .ThenInclude(od => od.Product)
                       .ToList();

        public Order GetById(int id)
            => _context.Orders
                       .Include(o => o.Customer)
                       .Include(o => o.OrderDetails)
                       .ThenInclude(od => od.Product)
                       .FirstOrDefault(o => o.Id == id);

        public void PlaceOrder(int customerId, string notes)
        {
            var cart = _context.Carts
                               .Include(c => c.CartItems)
                               .ThenInclude(ci => ci.Product)
                               .FirstOrDefault(c => c.CustomerId == customerId);

            if (cart == null || !cart.CartItems.Any()) return;

            var order = new Order
            {
                CustomerId = customerId,
                Notes = notes,
                TotalAmount = cart.CartItems.Sum(i => i.UnitPrice * i.Quantity),
                Status = OrderStatus.Pending
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            foreach (var item in cart.CartItems)
            {
                _context.OrderDetails.Add(new OrderDetail
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                });
            }

            _context.CartItems.RemoveRange(cart.CartItems);
            _context.SaveChanges();
        }

        public void UpdateStatus(int orderId, OrderStatus status)
        {
            var order = _context.Orders.Find(orderId);
            if (order != null)
            {
                order.Status = status;
                _context.SaveChanges();
            }
        }
    }
}