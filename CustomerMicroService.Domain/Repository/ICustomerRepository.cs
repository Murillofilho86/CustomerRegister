using CustomerMicroService.Domain.Entities;

namespace CustomerMicroService.Domain.Repository
{
    public interface ICustomerRepository 
    {
        Task<Customer> GetByIdAsync(Guid customerId);
        Task<Customer> AddAsync(Customer customer);
        void Delete(Customer customer);
    }
}
