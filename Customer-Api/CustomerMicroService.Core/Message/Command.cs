using FluentValidation.Results;
using MediatR;

namespace CustomerMicroService.Framework.Message
{
    public abstract class Command : IRequest<ValidationResult>
    {
        public DateTime Timestemp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            Timestemp = DateTime.Now;
        }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
