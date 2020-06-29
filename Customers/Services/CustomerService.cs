using System;
using System.Collections.Generic;
using System.Text;
using Customers.Models;
using System.Linq;
using System.Threading.Tasks;


namespace Customers.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IList<Customer> _customers;

        public CustomerService()
        {
            _customers = new List<Customer>
            {
                new Customer(1, "Tom"),
                new Customer(2, "Christine"),
                new Customer(3, "Henry"),
                new Customer(4, "Michael")
            };
        }

        public Customer GetCustomerById(int id)
        {
            return GetCustomerByIdAsync(id).Result;
        }

        public Task<Customer> GetCustomerByIdAsync(int id)
        {
            return Task.FromResult(_customers.Single(o => Equals(o.Id, id)));
        }

        public Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return Task.FromResult(_customers.AsEnumerable());
        }
    }

    public interface ICustomerService
    {
        Customer GetCustomerById(int id);
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<IEnumerable<Customer>> GetCustomersAsync();
    }
}
