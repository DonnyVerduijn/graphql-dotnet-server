using System;

namespace Orders.Models
{
    public class Order
    {
        public Order(string Id, string name, DateTime created, string description, string customerId )
        {
            this.Id = Id;
            Name = name;
            Description = description;
            Created = created;
            CustomerId = customerId;
            Status = OrderStatuses.CREATED;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; private set; }
        public string CustomerId { get; set; }
        public string Id { get; private set; }
        public OrderStatuses Status { get; private set; }

        public void Start()
        {
            Status = OrderStatuses.PROCESSING;
        }
    }

    [Flags]
    public enum OrderStatuses
    {
        CREATED = 2,
        PROCESSING = 4,
        COMPLETED = 8,
        CANCELLED = 16,
        CLOSED = 32
    }

}
