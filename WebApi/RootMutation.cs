using GraphQL.Types;
using WebApi.Customers.Schema;
using WebApi.Orders.Schema;


namespace WebApi
{
    // ReSharper disable once ClassNeverInstantiated.Global
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
