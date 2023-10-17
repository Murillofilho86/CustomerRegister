
namespace CustomerMicroService.Domain.Entities
{
    public class Customer  
    {
        public Customer()
        {
                CustomerId = Guid.NewGuid();
        }

        public Guid CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Address Address { get; set; }
    }
}
