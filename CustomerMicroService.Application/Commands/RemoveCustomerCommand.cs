using CustomerMicroService.Framework.Message;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerMicroService.Application.Commands
{
    public class RemoveCustomerCommand : BaseRequest, IRequest<IActionResult>
    {
        public Guid CustomerId { get; set; }
    }
}
