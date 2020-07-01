using Customers.Models;
using GraphQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IList<Customer> _customers;

        public CustomerService()
        {
            Random random = new Random();
            _customers = new List<Customer>
            {
                new Customer("1", "Tom", DateTime.Now.AddDays(random.Next(0, 10))),
                new Customer("2", "Christine", DateTime.Now.AddDays(random.Next(0, 10))),
                new Customer("3", "Henry", DateTime.Now.AddDays(random.Next(0, 10))),
                new Customer("4", "Michael", DateTime.Now.AddDays(random.Next(0, 10)))
            };
        }

        public Customer GetCustomerById(string id)
        {
            return GetCustomerByIdAsync(id).Result;
        }

        public Task<Customer> GetCustomerByIdAsync(string id)
        {
            Customer customer = _customers.SingleOrDefault(o => Equals(o.Id, id));
            return Task.FromResult(customer);
        }

      
        public Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return Task.FromResult(_customers.AsEnumerable());
        }

        public Task<Customer> CreateAsync(Customer customer)
        {
            _customers.Add(customer);
            return Task.FromResult(customer);
        }
    }

    public interface ICustomerService
    {
        Customer GetCustomerById(string id);
        Task<Customer> GetCustomerByIdAsync(string id);
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<Customer> CreateAsync(Customer customer);
    }
}
