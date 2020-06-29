using Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Services
{
    public class OrderService : IOrderService
    {
        private readonly IList<Order> _orders;
        private readonly IOrderEventService _events;

        public OrderService(IOrderEventService events)
        {
            _orders = new List<Order>
            {
                new Order("1000", "250 Conference brouchers", DateTime.Now, 1, "SSDF98SDFG98898SDDF"),
                new Order("2000", "250 T-shirts", DateTime.Now.AddHours(1), 2, "EOIKFHIS78687982DJHFJHF"),
                new Order("3000", "500 Stickers", DateTime.Now.AddHours(2), 3, "VJMEIJHDASHEJKDH87387SH"),
                new Order("4000", "10 Posters", DateTime.Now.AddHours(3), 4, "SGJEJKXJHD384XNBNX*3")
            };
            this._events = events;
        }

        public Task<Order> GetOrderByIdAsync(string id)
        {
            return Task.FromResult(_orders.Single(o => Equals(o.Id, id)));
        }

        public Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return Task.FromResult(_orders.AsEnumerable());
        }


        private Order GetById(string id)
        {
            var order = _orders.SingleOrDefault(o => Equals(o.Id, id));
            if (order == null)
            {
                throw new ArgumentException(string.Format(
                    "Order ID '{0}' is invalid", id));
            };
            return order;
        }

        public Task<Order> CreateAsync(Order order)
        {
            _orders.Add(order);
            var orderEvent = new OrderEvent(order.Id, order.Name, 
                OrderStatuses.CREATED, DateTime.Now);
            _events.AddEvent(orderEvent);
            return Task.FromResult(order);
        }

        public Task<Order> StartAsync(string orderId)
        {
            var order = GetById(orderId);
            order.Start();
            var orderEvent = new OrderEvent(order.Id, order.Name, 
                OrderStatuses.PROCESSING, DateTime.Now);
            _events.AddEvent(orderEvent);
           
            return Task.FromResult(order);
        }
    }

    public interface IOrderService
    {
        Task<Order> GetOrderByIdAsync(string id);
        Task<IEnumerable<Order>> GetOrdersAsync();
        Task<Order> CreateAsync(Order order);
        Task<Order> StartAsync(string orderId);
    }
}
