using FluentValidation.Results;
using MediatR;

namespace CustomerMicroService.Framework.Message
{
    public abstract class Command : IRequest<ValidationResult>
    {
        public ValidationResult ValidationResult { get; set; }

        public Command()
        {
            ValidationResult = new ValidationResult();
        }

        public virtual bool IsValid()
        {
            return ValidationResult.IsValid;
        }
    }
}
