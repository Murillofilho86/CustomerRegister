using CustomerMicroService.Data.Context;
using CustomerMicroService.Domain.Entities;
using CustomerMicroService.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace CustomerMicroService.Data.Repository
{
    public class CustomerRepository : SqlServerRepository<Customer>, ICustomerRepository
    {
        private readonly CustomerContext _context;
        protected DbSet<Customer> _dbSet;

        public CustomerRepository(CustomerContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Customer>();
        }

        public async Task<Customer> AddAsync(Customer customer)
        {
            var result = await _dbSet.AddAsync(customer);

            _context.SaveChanges();
            return result.Entity;
        }

        public bool Remove(Customer customer)
        {
            _dbSet.Remove(customer);
            return _context.SaveChanges() > 0;
        }

        public async Task<Customer> GetByIdAsync(Guid customerId)
        {
            return await _dbSet.Include(x => x.Address).FirstOrDefaultAsync(x => x.CustomerId == customerId);
        }

        public bool Update(Customer customer)
        {
            _dbSet.Update(customer);

            return _context.SaveChanges() > 0;
        }

        public async Task SaveAsync(Customer customer)
        {
            await _context.SaveChangesAsync();
        }
    }
}
