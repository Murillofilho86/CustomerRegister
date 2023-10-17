using AutoMapper;
using CustomerMicroService.Application.Commands;
using CustomerMicroService.Domain.Repository;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerMicroService.Application.Handlers
{
    public class CustomerCommandHandler : IRequestHandler<CreateCustomerCommand, IActionResult>,
        IRequestHandler<UpdateCustomerCommand, IActionResult>,
        IRequestHandler<RemoveCustomerCommand, IActionResult>
    {

        //private readonly ICustomerRepository _repository;
        //private readonly IMapper _mapper;

        //public CustomerCommandHandler(ICustomerRepository repository, IMapper mapper)
        //{
        //    _repository = repository;
        //    _mapper = mapper;
        //}

        public Task<IActionResult> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Handle(RemoveCustomerCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
