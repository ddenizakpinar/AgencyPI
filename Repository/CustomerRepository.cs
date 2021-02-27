using System.Collections.Generic;
using AgencyPI.Models;
using AgencyPI.Repository.IRepository;

namespace AgencyPI.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public bool CreateAgent(Customer customer)
        {
            throw new System.NotImplementedException();
        }

        public bool CustomerExists(int customerId)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteAgent(Customer customer)
        {
            throw new System.NotImplementedException();
        }

        public Customer GetCustomer(int customerId)
        {
            throw new System.NotImplementedException();
        }

        public List<Customer> GetCustomerInAgent(int agentId)
        {
            throw new System.NotImplementedException();
        }

        public List<Customer> GetCustomers()
        {
            throw new System.NotImplementedException();
        }

        public bool Save()
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateAgent(Customer customer)
        {
            throw new System.NotImplementedException();
        }
    }
}