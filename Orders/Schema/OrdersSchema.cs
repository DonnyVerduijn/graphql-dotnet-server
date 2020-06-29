
using GraphQL.Utilities;
using System;

namespace Orders.Schema
{
    public class OrdersSchema : GraphQL.Types.Schema
    {
        public OrdersSchema(IServiceProvider service) : base(service)
        {
            Query = service.GetRequiredService<OrdersQuery>();
            Mutation = service.GetRequiredService<OrdersMutation>();
            Subscription = service.GetRequiredService<OrdersSubscription>();
        }
    }
}
