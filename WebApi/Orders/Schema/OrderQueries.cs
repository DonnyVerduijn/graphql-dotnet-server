using GraphQL.Types;
using WebApi.Orders.Services;

namespace WebApi.Orders.Schema
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
