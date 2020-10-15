using GraphQL.Types;

namespace WebApi.Customers.Schema
{
    public class CustomerCreateInputType : InputObjectGraphType
    {
        public CustomerCreateInputType()
        {
            Name = "CustomerInput"; 
            Field<NonNullGraphType<StringGraphType>>("name");
        }
    }
}
