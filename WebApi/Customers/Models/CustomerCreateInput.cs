namespace WebApi.Customers.Models
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class CustomerCreateInput
    {
        public CustomerCreateInput(string name) {
            Name = name;
        }

        public string Name { get; }
       
    }
}
