using AutoMapper;
using CustomerMicroService.Application.Commands;
using CustomerMicroService.Domain.Entities;
using CustomerMicroService.Domain.Repository;
using CustomerMicroService.Framework.Result.Concrete;
using CustomerMicroService.Framework.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Numerics;

namespace CustomerMicroService.Application.Handlers
{
    public class CustomerCommandHandler : IRequestHandler<CreateCustomerCommand, IActionResult>,
        IRequestHandler<UpdateCustomerCommand, IActionResult>,
        IRequestHandler<RemoveCustomerCommand, IActionResult>
    {

        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;

        public CustomerCommandHandler(ICustomerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
        {
            ApplicationResult<bool> result = new ApplicationResult<bool>(command);

            Customer customer = await _repository.GetByIdAsync(command.CustomerId);

            if(customer == null)
            {
                result.Result = false;
                result.SetHttpStatusToNotFound("Cliente não encontrado.");
                return result;
            }

            customer.Update(command.FirstName, command.LastName, command.Email, command.Phone);

            result.Result = true;
            result.SetHttpStatusToOk("Cliente alterado com sucesso.");
            return result;
        }

        public async Task<IActionResult> Handle(RemoveCustomerCommand command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            ApplicationResult<bool> result = new ApplicationResult<bool>(command);


            Customer customer = new Customer(command.FirstName, command.LastName, command.Email, command.Phone, command.Cpf.OnlyNumbers(command.Cpf));
            Address address = _mapper.Map<Address>(command.Address);
            customer.AddAddress(address);

           await _repository.AddAsync(customer);
         
            result.Result = true;
            result.SetHttpStatusToOk("Catalogo alterado com sucesso.");
            return result;
        }
    }
}
