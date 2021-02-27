using System.Collections.Generic;
using System.Linq;
using AgencyPI.Data;
using AgencyPI.Models;
using AgencyPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace AgencyPI.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool CreateCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            return Save();
        }

        public bool CustomerExists(int customerId)
        {
            bool exists = _context.Customers.Any(x => x.Id == customerId);
            return exists;
        }
        public bool DeleteCustomer(Customer customer)
        {
            _context.Customers.Remove(customer);
            return Save();
        }

        public Customer GetCustomer(int customerId)
        {
            Customer customer = _context.Customers.AsNoTracking().FirstOrDefault(x => x.Id == customerId);
            return customer;
        }

        public List<Customer> GetCustomerInAgent(int agentId)
        {
            throw new System.NotImplementedException();
        }

        public List<Customer> GetCustomers()
        {
            List<Customer> customers = _context.Customers.OrderBy(x => x.Name).ToList();
            return customers;
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }
        public bool UpdateCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
            return Save();
        }
    }
}