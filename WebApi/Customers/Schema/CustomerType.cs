using GraphQL.Types;
using WebApi.Customers.Models;

namespace WebApi.Customers.Schema
{
    public sealed class CustomerType : ObjectGraphType<Customer>
    {
        public CustomerType()
        {
            Field(c => c.Id);
            Field(c => c.Name);

            // add orders field
        }
    }
}
