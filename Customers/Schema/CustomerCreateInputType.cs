using GraphQL.Types;
using System;

namespace Customers.Schema
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
