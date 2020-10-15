using GraphQL.Types;
using WebApi.Customers.Schema;
using WebApi.Orders.Schema;

namespace WebApi
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class RootQuery : ObjectGraphType<object>
    {
        public RootQuery()
        {
            Name = "Query";
            Field<OrderQueries>("Order",
                resolve: ctx => new { });

            Field<CustomerQueries>("Customer",
                resolve: ctx => new { });
        }
    }
}
