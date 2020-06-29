using System;
using System.Collections.Generic;
using System.Text;
using GraphQL.Types;
using Customers.Models;

namespace Customers.Schema
{
    public class CustomerType : ObjectGraphType<Customer>
    {
        public CustomerType()
        {
            Field(c => c.Id);
            Field(c => c.Name);
        }
    }
}
