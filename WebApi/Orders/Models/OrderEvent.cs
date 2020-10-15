using System;

namespace WebApi.Orders.Models
{
    public class OrderEvent
    {
        public OrderEvent(string orderId, string name, OrderStatuses status, DateTime timestamp)
        {
            OrderId = orderId;
            Name = name;
            Status = status;
            Timestamp = timestamp;
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; }
        public string OrderId { get; }
        public string Name { get; }
        public OrderStatuses Status { get; }
        public DateTime Timestamp { get; }
    }
}
