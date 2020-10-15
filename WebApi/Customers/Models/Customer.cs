using System;

namespace WebApi.Customers.Models
{
    public class Customer
    {
        public Customer(string id, string name, DateTime created)
        {
            Id = id;
            Name = name;
            Created = created;
        }

        public string Id { get; }
        public string Name { get; }
        public DateTime Created { get; }

    }
}
