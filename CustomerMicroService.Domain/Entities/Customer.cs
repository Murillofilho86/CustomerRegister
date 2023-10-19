
using CustomerMicroService.Framework.DomainObject;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerMicroService.Domain.Entities
{
    [Table("Customer")]
    public class Customer : EntityBase
    {
        public Customer(string firstName, string lastName, string email, string phone, Cpf cpf)
        {
            CustomerId = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Cpf = cpf;
        }

        [Key]
        [Column("CustomerId")]
        public Guid CustomerId { get; set; }

        [Column("FirstName")]
        public string FirstName { get; private set; }

        [Column("LastName")]
        public string LastName { get; private set; }

        [Column("Email")]
        public string Email { get; private set; }

        [Column("Phone")]
        public string Phone { get; private set; }

        [Column("Cpf")]
        public Cpf Cpf { get; set; }

        [Column("AddressId")]
        public Guid AddressId { get; set; }

     
        public virtual Address Address { get; private set; }

        public void AddAddress(Address address)
        {
            if (address is null) throw new DomainException("invalid address");
            Address = new Address(address.Street, address.Number, address.Complement, address.Neighborhood, address.City, address.State, address.ZipCode);
        }

        public void Update(string firstName, string lastName, string email, string phone)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
          
        }
    }
}


