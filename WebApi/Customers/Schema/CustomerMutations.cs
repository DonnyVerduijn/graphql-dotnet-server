using System;
using GraphQL;
using GraphQL.Types;
using WebApi.Customers.Models;
using WebApi.Customers.Services;


namespace WebApi.Customers.Schema
{
    public sealed class CustomerMutations : ObjectGraphType
    {
        public CustomerMutations(ICustomerService customers)
        {
            Name = "CustomerMutations";
            Field<CustomerType>(
            "create",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<CustomerCreateInputType>> { Name = "customer" }),
            resolve: ctx =>
            {
                CustomerCreateInput input = ctx.GetArgument<CustomerCreateInput>("customer");
                string id = Guid.NewGuid().ToString();
                Customer customer = new Customer(id, input.Name, DateTime.Now);
                return customers.CreateAsync(customer);
            }
         );
        }
    }
}
