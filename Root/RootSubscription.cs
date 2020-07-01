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
            IList<IEnumerable<FieldType>> subscriptions = new List<IEnumerable<FieldType>>();
            subscriptions.Add(provider.GetRequiredService<OrderSubscriptions>().Fields);
            subscriptions.Add(provider.GetRequiredService<CustomerSubscriptions>().Fields);
            foreach (IEnumerable<FieldType> set in subscriptions)
            {
                foreach (FieldType fieldType in set)
                {
                    AddField(fieldType);
                }
            }        
        }
    }
}
