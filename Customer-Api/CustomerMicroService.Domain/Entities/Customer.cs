
using CustomerMicroService.Domain.Entities;
using CustomerMicroService.Framework.DomainObject;
using CustomerMicroService.Framework.Message;
using System.ComponentModel.DataAnnotations;

namespace CustomerMicroService.Domain.Entities
{
 
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
    
 
        public Guid CustomerId { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Email { get; private set; }

        public string Phone { get; private set; }

        public Cpf Cpf { get; private set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
        public Address Address { get; private set; } 

        public void AddAddress(Address address)
        {
            if (address is null) throw new DomainException("invalid address");
            Address = new Address(address.Street, address.Number, address.Complement, address.Neighborhood, address.City, address.State, address.ZipCode);
        }

        public void Update(string firstName, string lastName, string email, string phone, Address address)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            ModifiedIn = DateTime.Now;
            //Address = 
        }
    }
}


