using CustomerMicroService.Application.ViewModel;
using CustomerMicroService.Framework.Message;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerMicroService.Application.Commands
{
    public class CreateCustomerCommand : BaseActionRequest, IRequest<IActionResult>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty; 
        public AddressViewModel Address { get; set; }
    }
}
