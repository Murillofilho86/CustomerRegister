using CustomerMicroService.Application.ViewModel;
using CustomerMicroService.Framework.DomainObject;
using CustomerMicroService.Framework.Message;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerMicroService.Application.Commands
{
    public class CreateCustomerCommand :  BaseRequest,   IRequest<IActionResult>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Cpf { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty; 
        public AddressViewModel Address { get; set; }


        public bool IsValid()
        {
            ValidationResult = new CreateCustomerValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }



    public class CreateCustomerValidation : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerValidation()
        {
            RuleFor(c => c.FirstName)
              .NotEmpty()
                    .WithMessage("O Primeiro nome não foi informado");

            RuleFor(c => c.LastName)
             .NotEmpty()
            .WithMessage("O Sobrenome não foi informado");

            RuleFor(c => c.Cpf)
                 .Must(CpfIsValid)
                 .WithMessage("O CPF informado não é válido.");

            RuleFor(c => c.Email)
                  .Must(EmailIsValid)
                  .WithMessage("O E-mail informado não é válido.");
        }

        protected static bool CpfIsValid(string cpf)
        {
            return Cpf.IsValid(cpf);
        }

        protected static bool EmailIsValid(string email)
        {
            return Email.Validar(email);
        }
    }
}
