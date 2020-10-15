using GraphQL.Types;
using WebApi.Customers.Models;

namespace WebApi.Customers.Schema
{
    public sealed class CustomerEventType : ObjectGraphType<CustomerEvent>
    {
        public CustomerEventType()
        {
            Field(e => e.Id);
            Field(e => e.Name);
            Field(e => e.CustomerId);      
            Field(e => e.Timestamp);
        }


    }
}
