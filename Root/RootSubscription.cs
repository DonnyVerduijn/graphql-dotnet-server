using Customers.Schema;
using GraphQL.Types;
using GraphQL.Utilities;
using Orders.Schema;
using System;
using System.Collections.Generic;

namespace Root
{
    public class RootSubscription : ObjectGraphType<object>   
    {
        public RootSubscription(IServiceProvider provider)
        {
            Name = "Subscription";
            IEnumerable<FieldType> orderSubs = provider.GetRequiredService<OrderSubscriptions>().Fields;
            IEnumerable<FieldType> customerSubs = provider.GetRequiredService<CustomerSubscriptions>().Fields;
            foreach (FieldType fieldType in orderSubs)
            {
                AddField(fieldType);
            }
            foreach(FieldType fieldType in customerSubs)
            {
                AddField(fieldType);
            }
        }
    }
}
