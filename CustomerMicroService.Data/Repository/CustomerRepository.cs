using CustomerMicroService.Data.Context;
using CustomerMicroService.Domain.Entities;
using CustomerMicroService.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace CustomerMicroService.Data.Repository
{
    public class CustomerRepository : SqlServerRepository<Customer>, ICustomerRepository
    {
        private readonly CustomerContext _context;

        public CustomerRepository(CustomerContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Customer> AddAsync(Customer customer)
        {
            var result = await Entity.AddAsync(customer);
            _context.SaveChanges();
            return result.Entity;
        }

        public void Delete(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Deleted;
        }

        public async Task<Customer> GetByIdAsync(Guid customerId)
        {
            return await Entity.FindAsync(customerId);
        }
    }
}
