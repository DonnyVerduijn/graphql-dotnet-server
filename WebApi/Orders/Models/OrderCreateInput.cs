using System;

namespace WebApi.Orders.Models
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class OrderCreateInput
    {
        public OrderCreateInput(string name, string description, string customerId, DateTime created) {
            Created = created;
            CustomerId = customerId;
            Description = description;
            Name = name;
        }

        public string Name { get; }
        public string Description { get; }
        public string CustomerId { get; }
        public DateTime Created { get; }

    }
}
