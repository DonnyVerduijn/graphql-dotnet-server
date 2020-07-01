using GraphQL.Utilities;
using System;

namespace Root
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
