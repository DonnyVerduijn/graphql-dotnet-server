using System;

namespace Orders.Models
{
    public class OrderCreateInput
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CustomerId { get; set; }
        public DateTime Created { get; private set; }

        public string Id { get; private set; }
        public Schema.OrderStatusesEnum Status { get; private set; }

    }
}
