using System;

namespace WebApi.Customers.Models
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

        public string Id { get; }
        public string CustomerId { get; }
        public string Name { get; }
        public DateTime Timestamp { get; }
    }
}
