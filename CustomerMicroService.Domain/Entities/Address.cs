namespace CustomerMicroService.Domain.Entities
{
    public class Address 
    {
        public Address()
        {
                AddressId = Guid.NewGuid();
        }

        public Guid AddressId { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string City { get; set; }

        public string State { get; set; }
        public string Zip { get; set; }
    }
}
