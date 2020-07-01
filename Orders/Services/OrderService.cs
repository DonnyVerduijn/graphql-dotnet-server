using Customers.Models;
using Customers.Services;
using GraphQL.Utilities;
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
        private readonly ICustomerService _customers;
        private readonly IOrderEventService _events;

        public OrderService(IServiceProvider service)
        {
            _orders = new List<Order>
            {
                new Order("1000", "250 Conference brouchers", DateTime.Now, "SSDF98SDFG98898SDDF", "1"),
                new Order("2000", "250 T-shirts", DateTime.Now.AddHours(1), "EOIKFHIS78687982DJHFJHF", "2"),
                new Order("3000", "500 Stickers", DateTime.Now.AddHours(2), "VJMEIJHDASHEJKDH87387SH", "3"),
                new Order("4000", "10 Posters", DateTime.Now.AddHours(3), "SGJEJKXJHD384XNBNX*3", "4")
            };
            _events = service.GetRequiredService<IOrderEventService>();
            _customers = service.GetRequiredService<ICustomerService>();
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
                string message = string.Format("Order ID '{0}' is invalid", id);
                throw new ArgumentException(message);
            };
            return order;
        }

        public Task<Order> CreateAsync(Order order)
        {
            Customer customer = _customers.GetCustomerById(order.CustomerId);
            if (Equals(customer, null))
            {
                string message = string.Format("Customer ID '{0}' is invalid", order.CustomerId);
                throw new ArgumentException(message);
            }
            else
            {
                _orders.Add(order);
                var orderEvent = new OrderEvent(order.Id, order.Name,
                    OrderStatuses.CREATED, DateTime.Now);
                _events.AddEvent(orderEvent);
                return Task.FromResult(order);
            }
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
