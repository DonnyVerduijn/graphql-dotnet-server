using GraphQL;
using GraphQL.Resolvers;
using GraphQL.Subscription;
using GraphQL.Types;
using Customers.Models;
using Customers.Services;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;

namespace Customers.Schema
{
    public class CustomerSubscriptions : ObjectGraphType
    {
        private readonly ICustomerEventService _events;

        public CustomerSubscriptions(ICustomerEventService events)
        {
            _events = events;
            Name = "CustomerSubscriptions";
            AddField(new EventStreamFieldType
            {
                Name = "customerEvent",               
                Type = typeof(CustomerEventType),
                Resolver = new FuncFieldResolver<CustomerEvent>(ResolveEvent),
                Subscriber = new EventStreamResolver<CustomerEvent>(Subscribe)
            });
        }

        private CustomerEvent ResolveEvent(IResolveFieldContext context)
        {
            var customerEvent = context.Source as CustomerEvent;
            return customerEvent;
        }

        private IObservable<CustomerEvent> Subscribe(IResolveEventStreamContext context)
        {
            return _events.EventStream();
        }
    }
}
