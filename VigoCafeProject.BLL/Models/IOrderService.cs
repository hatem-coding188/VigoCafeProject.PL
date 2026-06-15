using System.Collections.Generic;
using DAL.Models;

using Order = DAL.Models.Order;

namespace BLL.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAll();
        IEnumerable<Order> GetByCustomerId(int customerId);
        Order GetById(int id);
        void PlaceOrder(int customerId, string notes);
        void UpdateStatus(int orderId, OrderStatus status);
    }
}