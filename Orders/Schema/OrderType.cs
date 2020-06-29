using GraphQL.Types;
using Orders.Models;
using Orders.Services;

namespace Orders.Schema
{
    public class OrderType : ObjectGraphType<Order>
    {
        public OrderType()
        {
            CreateFields();
        }

        public OrderType(ICustomerService customers)
        {
            CreateFields();
            Field<CustomerType>(
                "customer",
                resolve: ctx => customers.GetCustomerByIdAsync(ctx.Source.CustomerId));
          
        }


        private void CreateFields()
        {
            Field(o => o.Id);
            Field(o => o.Name);
            Field(o => o.Description);
            Field(o => o.Created);
            Field<OrderStatusesEnum>("status",
                resolve: context => context.Source.Status
                );
        }
    }
}
