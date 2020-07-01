using GraphQL;
using GraphQL.Types;
using Orders.Models;
using Orders.Services;
using System;

namespace Orders.Schema
{
    public class OrderMutations : ObjectGraphType
    {
        public OrderMutations(IOrderService orders)
        {
            Name = "OrderMutations";
            Field<OrderType>(
            "create",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<OrderCreateInputType>>
                { Name = "order" }),
            resolve: ctx =>
            {
                var input = ctx.GetArgument<OrderCreateInput>("order");
                var orderId = Guid.NewGuid().ToString();
                var order = new Order(orderId, input.Name, input.Created, input.Description, input.CustomerId);
                return orders.CreateAsync(order);
            }
         );

            FieldAsync<OrderType>(
                "start",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> 
                    { Name = "orderId" }),
                resolve: async context =>
                {
                    var orderId = context.GetArgument<string>("orderId");
                    return await context.TryAsyncResolve(
                        async c => await orders.StartAsync(orderId));


                });
        }
    }
}
