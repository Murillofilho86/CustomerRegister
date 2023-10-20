using CustomerMicroService.Data.Mapping;
using CustomerMicroService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerMicroService.Data.Context
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options) { }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Address> Address { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerMapping());
    

            base.OnModelCreating(modelBuilder);
        }
    }
}
