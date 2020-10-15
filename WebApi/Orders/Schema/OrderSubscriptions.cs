using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using GraphQL;
using GraphQL.Resolvers;
using GraphQL.Subscription;
using GraphQL.Types;
using WebApi.Orders.Models;
using WebApi.Orders.Services;

namespace WebApi.Orders.Schema {
    public sealed class OrderSubscriptions : ObjectGraphType<object> {
        private readonly IOrderEventService _events;

        public OrderSubscriptions(IOrderEventService events) {
            _events = events;
            Name = "OrderSubscriptions";
            AddField(new EventStreamFieldType {
                Name = "orderEvent",
                Arguments = new QueryArguments(new QueryArgument<ListGraphType<OrderStatusesEnum>> {
                    Name = "statuses"
                }),
                Type = typeof(OrderEventType),
                Resolver = new FuncFieldResolver<OrderEvent>(ResolveEvent),
                Subscriber = new EventStreamResolver<OrderEvent>(Subscribe)
            });
            AddField(new EventStreamFieldType {
                Name = "ordersClosed",
                Type = typeof(ListGraphType<OrderEventType>),
                Resolver = new FuncFieldResolver<OrderEvent>(ResolveEvent),
                Subscriber = new EventStreamResolver<OrderEvent>(SubscribeClosed)
            });
        }

        private static OrderEvent ResolveEvent(IResolveFieldContext context) {
            OrderEvent orderEvent = context.Source as OrderEvent;
            return orderEvent;
        }

        private IObservable<OrderEvent> SubscribeClosed(IResolveEventStreamContext context) {
            return _events.EventStream();
        }

        private IObservable<OrderEvent> Subscribe(IResolveEventStreamContext context) {
            IList<OrderStatuses> statusList = context.GetArgument<IList<OrderStatuses>>("statuses",
                new List<OrderStatuses>());

            if (statusList.Count <= 0) return _events.EventStream();
            OrderStatuses statuses = statusList.Aggregate<OrderStatuses, OrderStatuses>(0, (current, status) => current | status);
            return _events.EventStream().Where(e => (e.Status & statuses) == e.Status);
        }
    }
}