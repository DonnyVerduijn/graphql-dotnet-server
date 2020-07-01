using GraphQL.Types;
using Customers.Schema;
using Orders.Schema;


namespace Root
{
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
