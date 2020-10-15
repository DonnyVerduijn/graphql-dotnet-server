using System;
using GraphQL.Utilities;

namespace WebApi
{
    public class Schema : GraphQL.Types.Schema
    {
        public Schema(IServiceProvider service) : base(service)
        {
            Query = service.GetRequiredService<RootQuery>();
            Mutation = service.GetRequiredService<RootMutation>();
            Subscription = service.GetRequiredService<RootSubscription>();
        }
    }
}
