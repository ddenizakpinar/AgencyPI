using System.Collections.Generic;
using AgencyPI.Models;

namespace AgencyPI.Repository.IRepository
{
    public interface ICustomerRepository
    {
        Customer GetCustomer(int customerId);
        List<Customer> GetCustomers();
        bool CreateAgent(Customer customer);
        bool UpdateAgent(Customer customer);
        bool DeleteAgent(Customer customer);
        List<Customer> GetCustomerInAgent(int agentId);
        bool Save();
    }
}