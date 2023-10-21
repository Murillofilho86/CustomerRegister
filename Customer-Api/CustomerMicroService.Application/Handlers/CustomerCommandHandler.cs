using AutoMapper;
using CustomerMicroService.Application.Commands;
using CustomerMicroService.Domain.Entities;
using CustomerMicroService.Domain.Repository;
using CustomerMicroService.Framework.Result.Concrete;
using CustomerMicroService.Framework.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

            if (customer == null)
            {
                result.Result = false;
                result.SetHttpStatusToNotFound("Cliente não encontrado.");
                return result;
            }
            Address address = _mapper.Map<Address>(command.Address);

            customer.Update(command.FirstName, command.LastName, command.Email, command.Phone, address);

            customer.Address.Update(address.Street, address.Number, address.Complement, address.Neighborhood, address.City, address.State, address.ZipCode);

            var updateSucess = _repository.Update(customer);
            if (updateSucess)
            {
                result.Result = true;
                result.SetHttpStatusToOk("Cliente alterado com sucesso.");
                return result;
            }
            result.Result = false;
            result.SetHttpStatusToBadRequest();
            return result;
        }

        public async Task<IActionResult> Handle(RemoveCustomerCommand command, CancellationToken cancellationToken)
        {
            ApplicationResult<bool> result = new ApplicationResult<bool>(command);

            Customer customer = await _repository.GetByIdAsync(command.CustomerId);

            if (customer == null)
            {
                result.Result = false;
                result.SetHttpStatusToNotFound("Cliente não encontrado.");
                return result;
            }
 
            
            var deleteSucess = _repository.Remove(customer);
            if (deleteSucess)
            {
                result.Result = true;
                result.SetHttpStatusToOk("Cliente excluido com sucesso.");
                return result;
            }

            result.Result = false;
            result.SetHttpStatusToBadRequest();
            return result;
        }

        public async Task<IActionResult> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            ApplicationResult<bool> result = new ApplicationResult<bool>(command);


            Customer customer = new Customer(command.FirstName, command.LastName, command.Email, command.Phone, command.Cpf.OnlyNumbers(command.Cpf));
            Address address = _mapper.Map<Address>(command.Address);
            customer.AddAddress(address);

            await _repository.AddAsync(customer);

            result.Result = true;
            result.SetHttpStatusToOk("Cliente registradocom sucesso.");
            return result;
        }
    }
}
