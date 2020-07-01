using GraphQL.Types;
using Orders.Services;

namespace Orders.Schema
{
    public class OrderQueries : ObjectGraphType
    {
        public OrderQueries(IOrderService orders)
        {
            Name = "OrderQueries";
            Field<ListGraphType<OrderType>>("getAll",
                resolve: ctx => orders.GetOrdersAsync()
            );
        }
    }
}
