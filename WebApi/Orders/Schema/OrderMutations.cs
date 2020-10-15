using System;
using GraphQL;
using GraphQL.Types;
using WebApi.Orders.Models;
using WebApi.Orders.Services;

namespace WebApi.Orders.Schema {
    public class OrderMutations : ObjectGraphType {
        public OrderMutations(IOrderService orders) {
            Name = "OrderMutations";
            
            Field<OrderType>(
                "create",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<OrderCreateInputType>> { Name = "order" }),
                resolve: ctx => {
                    OrderCreateInput input = ctx.GetArgument<OrderCreateInput>("order");
                    string orderId = Guid.NewGuid().ToString();
                    Order order = new Order(orderId, input.Name, input.Created, input.Description, input.CustomerId);
                    return orders.CreateAsync(order);
                }
             );

            FieldAsync<OrderType>(
                "start",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "orderId" }),
                resolve: async context => {
                    string orderId = context.GetArgument<string>("orderId");
                    return await context.TryAsyncResolve(
                        async c => await orders.StartAsync(orderId));
                });

            FieldAsync<OrderType>(
                "close",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "orderId" }),
                resolve: async context => {
                    string orderId = context.GetArgument<string>("orderId");
                    return await context.TryAsyncResolve(
                        async c => await orders.CloseAsync(orderId));


                });
        }
    }
}
