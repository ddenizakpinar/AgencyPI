using System.Collections.Generic;
using AgencyPI.Models;
using AgencyPI.Repository.IRepository;

namespace AgencyPI.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public bool CreateOrder(Order order)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteOrder(Order order)
        {
            throw new System.NotImplementedException();
        }

        public List<Order> GetCustomerInAgent(int agentId)
        {
            throw new System.NotImplementedException();
        }

        public List<Order> GetCustomerInCustomer(int customerId)
        {
            throw new System.NotImplementedException();
        }

        public Order GetOrder(int orderId)
        {
            throw new System.NotImplementedException();
        }

        public List<Order> GetOrders()
        {
            throw new System.NotImplementedException();
        }

        public bool Save()
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateOrder(Order order)
        {
            throw new System.NotImplementedException();
        }
    }
}