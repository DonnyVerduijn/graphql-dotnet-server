using System;

namespace WebApi.Orders.Models {
    public class Order {
        public Order(string id, string name, DateTime created, string description, string customerId) {
            Id = id;
            Name = name;
            Description = description;
            Created = created;
            CustomerId = customerId;
            Status = OrderStatuses.Created;
        }

        public string Name { get; }
        public string Description { get; }
        public DateTime Created { get; }
        public string CustomerId { get; }
        public string Id { get; }
        public OrderStatuses Status { get; private set; }

        public void Start() {
            Status = OrderStatuses.Processing;
        }

        public void Close() {
            Status = OrderStatuses.Closed;
        }
    }

    [Flags]
    public enum OrderStatuses {
        Created = 2,
        Processing = 4,
        Completed = 8,
        Cancelled = 16,
        Closed = 32
    }
}