using AutoMapper;
using CustomerMicroService.Application.Commands;
using CustomerMicroService.Application.ViewModel;
using CustomerMicroService.Domain.Entities;

namespace CustomerMicroService.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CreateCustomerCommand, Customer>();
            CreateMap<UpdateCustomerCommand, Customer>();

            CreateMap<AddressViewModel, Address>();
        }
    }
}
