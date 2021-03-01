using System.Collections.Generic;
using AgencyPI.Models;

namespace AgencyPI.Repository.IRepository
{
    public interface ICustomerRepository
    {
        Customer GetCustomer(int? customerId);
        List<Customer> GetCustomers();
        bool CreateCustomer(Customer customer);
        bool UpdateCustomer(Customer customer);
        bool DeleteCustomer(Customer customer);
        List<Customer> GetCustomersByAgent(int agentId);
        bool CustomerExists(int customerId);
        bool Save();
    }
}