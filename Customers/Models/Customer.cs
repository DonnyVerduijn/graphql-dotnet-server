using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Customers.Models
{
    public class Customer
    {
        public Customer(string id, string name, DateTime created)
        {
            Id = id;
            Name = name;
            Created = created;
        }

        public string Id { get; private set; }
        public string Name { get; set; }
        public DateTime Created { get; private set; }

    }
}
