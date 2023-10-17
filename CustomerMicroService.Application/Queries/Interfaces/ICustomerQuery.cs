using CustomerMicroService.Application.Requests;
using CustomerMicroService.Application.ViewModel;
using CustomerMicroService.Framework.Result.Interface;
using CustomerMicroService.Framework.ViewModel;

namespace CustomerMicroService.Application.Queries.Interfaces
{
    public interface ICustomerQuery
    {
        Task<IApplicationResult<DadosPaginadosViewModel<CustomerViewModel>>> GetByFilterAsync(CustomerFilterRequest request);
        Task<IApplicationResult<CustomerViewModel>> GetByIdAsync(Guid customerId);
    }
}
