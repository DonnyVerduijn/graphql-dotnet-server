using GraphQL.Types;

namespace WebApi.Orders.Schema
{
    public class OrderCreateInputType : InputObjectGraphType
    {
        public OrderCreateInputType()
        {
            Name = "OrderInput";
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<NonNullGraphType<StringGraphType>>("description");
            Field<NonNullGraphType<StringGraphType>>("customerId");
            Field<NonNullGraphType<DateGraphType>>("created");
        }
    }
}
