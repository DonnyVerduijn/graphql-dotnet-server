using Customers.Models;
using GraphQL.Types;


namespace Customers.Schema
{
    public class CustomerEventType : ObjectGraphType<CustomerEvent>
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
