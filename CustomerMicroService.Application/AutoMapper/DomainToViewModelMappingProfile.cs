using AutoMapper;
using CustomerMicroService.Application.ViewModel;
using CustomerMicroService.Domain.Entities;

namespace CustomerMicroService.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Customer, CustomerViewModel>();
        }
    }
}
