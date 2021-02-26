using System.Collections.Generic;
using AgencyPI.Models;

namespace AgencyPI.Repository.IRepository
{
    public interface IOrderRepository
    {
        Order GetOrder(int orderId);
        List<Order> GetOrders();
        bool CreateOrder(Order order);
        bool UpdateOrder(Order order);
        bool DeleteOrder(Order order);
        List<Order> GetCustomerInAgent(int agentId);
        List<Order> GetCustomerInCustomer(int customerId);
        bool Save();
    }
}