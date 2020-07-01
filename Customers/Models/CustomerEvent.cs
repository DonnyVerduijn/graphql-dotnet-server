using System;
using System.Collections.Generic;
using System.Text;

namespace Customers.Models
{
    public class CustomerEvent
    {
        public CustomerEvent(string customerId, string name, DateTime timestamp)
        {
            CustomerId = customerId;
            Name = name;
            Timestamp = timestamp;
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public string CustomerId { get; private set; }
        public string Name { get; set; }
        public DateTime Timestamp { get; private set; }
    }
}
