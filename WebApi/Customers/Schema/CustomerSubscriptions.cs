using System;
using GraphQL;
using GraphQL.Resolvers;
using GraphQL.Subscription;
using GraphQL.Types;
using WebApi.Customers.Models;
using WebApi.Customers.Services;

namespace WebApi.Customers.Schema
{
    public sealed class CustomerSubscriptions : ObjectGraphType
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

        private static CustomerEvent ResolveEvent(IResolveFieldContext context)
        {
            CustomerEvent customerEvent = context.Source as CustomerEvent;
            return customerEvent;
        }

        private IObservable<CustomerEvent> Subscribe(IResolveEventStreamContext context)
        {
            return _events.EventStream();
        }
    }
}
