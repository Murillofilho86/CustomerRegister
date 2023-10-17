using CustomerMicroService.Data.Context;
using CustomerMicroService.Domain.Entities;
using CustomerMicroService.Domain.Repository;

namespace CustomerMicroService.Data.Repository
{
    public class CustomerRepository : SqlServerRepository<Customer>, ICustomerRepository
    {
        private readonly CustomerContext _context;

        public CustomerRepository(CustomerContext context) : base(context)
        {
            _context = context;
        }

    }
}
