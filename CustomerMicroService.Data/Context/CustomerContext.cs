using CustomerMicroService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerMicroService.Data.Context
{
    public class CustomerContext : DbContext
    {

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasKey(c => c.CustomerId);
            modelBuilder.Entity<Address>().HasKey(a => a.AddressId);


            modelBuilder.Entity<Customer>()
                       .HasOne(c => c.Address);

        }
    }
}
