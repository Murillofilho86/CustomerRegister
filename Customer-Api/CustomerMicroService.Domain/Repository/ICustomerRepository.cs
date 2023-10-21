using CustomerMicroService.Domain.Entities;

namespace CustomerMicroService.Domain.Repository
{
    public interface ICustomerRepository
    {
        Task<Customer> GetByIdAsync(Guid customerId);
        Task<Customer> AddAsync(Customer customer);
        bool Update(Customer customer);
        bool Remove(Customer customer);
        Task SaveAsync(Customer customer);
    }
}
