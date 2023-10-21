namespace CustomerMicroService.Domain.Entities
{

    public class Address  
    {
        public Address(string street, string number, string complement, string neighborhood, string city, string state, string zipCode)
        {
            AddressId = Guid.NewGuid();
            Street = street;
            Number = number;
            Complement = complement;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            ZipCode = zipCode;
        }

 
        public Guid AddressId { get; set; }

        public string Street { get; private set; }

 
        public string Number { get; private set; }

 
        public string Complement {  get; private set; }

 
        public string Neighborhood { get; private set; }

    
        public string City { get; private set; }
 
        public string State { get; private set; }

         
        public string ZipCode { get; private set; }

        public void Update(string street, string number, string complement, string neighborhood, string city, string state, string zipCode)
        {
            Street = street;
            Number = number;
            Complement = complement;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            ZipCode = zipCode;
        }
    }
}
