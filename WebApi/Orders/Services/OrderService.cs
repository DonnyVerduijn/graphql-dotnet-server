using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Utilities;
using WebApi.Customers.Models;
using WebApi.Customers.Services;
using WebApi.Orders.Models;

namespace WebApi.Orders.Services {
    public class OrderService : IOrderService {
        private readonly IList<Order> _orders;
        private readonly ICustomerService _customers;
        private readonly IOrderEventService _events;

        // suppress description typo errors
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public OrderService(IServiceProvider provider) {
            _orders = new List<Order>
            {
                new Order("1000", "250 Conference vouchers", DateTime.Now, "SSDF98SDFG98898SDDF", "1"),
                new Order("2000", "250 T-shirts", DateTime.Now.AddHours(1), "EOIKFHIS78687982DJHFJHF", "2"),
                new Order("3000", "500 Stickers", DateTime.Now.AddHours(2), "VJMEIJHDASHEJKDH87387SH", "3"),
                new Order("4000", "10 Posters", DateTime.Now.AddHours(3), "SGJEJKXJHD384XNBNX*3", "4")
            };
            _events = provider.GetRequiredService<IOrderEventService>();
            _customers = provider.GetRequiredService<ICustomerService>();
        }

        public Task<Order> GetOrderByIdAsync(string id) {
            return Task.FromResult(_orders.Single(o => Equals(o.Id, id)));
        }

        public Task<IEnumerable<Order>> GetOrdersAsync() {
            return Task.FromResult(_orders.AsEnumerable());
        }

        private Order GetById(string id) {
            Order order = _orders.SingleOrDefault(o => Equals(o.Id, id));
            if (order != null) return order;
            string message = string.Format($"Order ID '{id}' is invalid");
            throw new ArgumentException(message);
        }

        public Task<Order> CloseAsync(string id) {
            Order order = _orders.SingleOrDefault(o => Equals(o.Id, id));      
            if (order == null) {
                string message = string.Format($"Order ID '{id}' cannot be deleted");
                throw new ArgumentException(message);
            }

            order.Close();
            OrderEvent e = new OrderEvent(order.Id, order.Name, OrderStatuses.Closed, order.Created);
            _events.AddEvent(e);
            _orders.Remove(order);
            return Task.FromResult(order);
        }

        public Task<Order> CreateAsync(Order order) {
            Customer customer = _customers.GetCustomerById(order.CustomerId);
            if (Equals(customer, null)) {
                string message = $"Customer ID '{order.CustomerId}' is invalid";
                throw new ArgumentException(message);
            }

            _orders.Add(order);
            OrderEvent orderEvent = new OrderEvent(order.Id, order.Name,
                OrderStatuses.Created, DateTime.Now);
            _events.AddEvent(orderEvent);
            return Task.FromResult(order);
        }

        public Task<Order> StartAsync(string orderId) {
            Order order = GetById(orderId);
            order.Start();
            OrderEvent orderEvent = new OrderEvent(order.Id, order.Name,
                OrderStatuses.Processing, DateTime.Now);
            _events.AddEvent(orderEvent);

            return Task.FromResult(order);
        }
    }

    public interface IOrderService {
        Task<Order> GetOrderByIdAsync(string id);
        Task<IEnumerable<Order>> GetOrdersAsync();
        Task<Order> CreateAsync(Order order);
        Task<Order> StartAsync(string orderId);
        Task<Order> CloseAsync(string orderId);
    }
}
