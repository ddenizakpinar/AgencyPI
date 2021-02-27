using System.Collections.Generic;
using System.Linq;
using AgencyPI.Data;
using AgencyPI.Models;
using AgencyPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace AgencyPI.Repository
{
    public class OrderRepository : IOrderRepository
    {

        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            return Save();
        }

        public bool DeleteOrder(Order order)
        {
            _context.Orders.Remove(order);
            return Save();
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
            Order order = _context.Orders.AsNoTracking().FirstOrDefault(x => x.Id == orderId);
            return order;
        }

        public List<Order> GetOrders()
        {
            List<Order> orders = _context.Orders.OrderBy(x => x.Id).ToList();
            return orders;
        }

        public bool OrderExists(int orderId)
        {
            bool exists = _context.Orders.Any(x => x.Id == orderId);
            return exists;
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            return Save();
        }
    }
}