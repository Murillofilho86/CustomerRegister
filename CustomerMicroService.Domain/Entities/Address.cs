using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerMicroService.Domain.Entities
{
    [Table("Address")]
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

        [Key]
        [Column("AddressId")]
        public Guid AddressId { get; set; }

        [Column("Street")]
        public string Street { get; private set; }

        [Column("Number")]
        public string Number { get; private set; }

        [Column("Complement")]
        public string Complement {  get; private set; }

        [Column("Neighborhood")]
        public string Neighborhood { get; private set; }

        [Column("City")]
        public string City { get; private set; }

        [Column("State")]
        public string State { get; private set; }

        [Column("Zipcode")]
        public string ZipCode { get; private set; }

    }
}
