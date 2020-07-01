using Customers.Models;
using Customers.Services;
using GraphQL;
using GraphQL.Types;
using System;


namespace Customers.Schema
{
    public class CustomerMutations : ObjectGraphType
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
                var input = ctx.GetArgument<CustomerCreateInput>("customer");
                var id = Guid.NewGuid().ToString();
                var customer = new Customer(id, input.Name, DateTime.Now);
                return customers.CreateAsync(customer);
            }
         );
        }
    }
}
