using System;
using System.Collections.Generic;
using GraphQL.Types;
using GraphQL.Utilities;
using WebApi.Customers.Schema;
using WebApi.Orders.Schema;

namespace WebApi {
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class RootSubscription : ObjectGraphType<object> {
        public RootSubscription(IServiceProvider provider) {
            Name = "Subscription";
            IList<IEnumerable<FieldType>> subscriptions = new List<IEnumerable<FieldType>>
            {
                provider.GetRequiredService<OrderSubscriptions>().Fields,
                provider.GetRequiredService<CustomerSubscriptions>().Fields
            };
            foreach (IEnumerable<FieldType> set in subscriptions) {
                foreach (FieldType fieldType in set) {
                    AddField(fieldType);
                }
            }
        }
    }
}
