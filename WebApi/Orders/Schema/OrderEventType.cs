using GraphQL.Types;
using WebApi.Orders.Models;

namespace WebApi.Orders.Schema
{
    public sealed class OrderEventType : ObjectGraphType<OrderEvent>
    {
        public OrderEventType()
        {
            Field(e => e.Id);
            Field(e => e.Name);
            Field(e => e.OrderId);
            Field<OrderStatusesEnum>("status",
                resolve: ctx => ctx.Source.Status);
            Field(e => e.Timestamp);
        }
        

    }
}
