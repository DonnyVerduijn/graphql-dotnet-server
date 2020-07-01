using Customers.Schema;
using GraphQL.Types;
using Orders.Schema;


namespace Root
{
    public class RootMutation : ObjectGraphType<object>
    { 
        public RootMutation()
        {
            Name = "Mutation";
            Field<OrderMutations>("Order",
                resolve: ctx => new { });

            Field<CustomerMutations>("Customer",
                resolve: ctx => new { });
        }
    }
}
