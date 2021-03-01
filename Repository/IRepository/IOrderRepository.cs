using System.Collections.Generic;
using AgencyPI.Models;

namespace AgencyPI.Repository.IRepository
{
    public interface IOrderRepository
    {
        Order GetOrder(int? orderId);
        List<Order> GetOrders();
        bool CreateOrder(Order order);
        bool UpdateOrder(Order order);
        bool DeleteOrder(Order order);
        List<Order> GetOrdersByCustomer(int customerId);
        bool OrderExists(int orderId);
        bool Save();
    }
}