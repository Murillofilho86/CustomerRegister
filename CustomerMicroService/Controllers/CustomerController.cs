using CustomerMicroService.Application.Commands;
using CustomerMicroService.Application.Queries.Interfaces;
using CustomerMicroService.Application.Requests;
using CustomerMicroService.Framework.Result.Interface;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerMicroService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private IMediator _mediator { get; }
        private readonly ICustomerQuery _query;

        public CustomerController(IMediator mediator, ICustomerQuery query)
        {
            _mediator = mediator;
            _query = query;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] CustomerFilterRequest request)
         => await _query.GetByFilterAsync(request);

        [HttpGet("{customerId:guid}")]
        public async Task<IActionResult> GetByUnidadeId([FromRoute] Guid customerId)
          => await _query.GetByIdAsync(customerId);


        [HttpPost(), Produces("application/json", Type = typeof(IApplicationResult<Guid>))]
        public async Task<IActionResult> PostCatalogo([FromBody] CreateCustomerCommand command)
               => await _mediator.Send(command);


        [HttpPut("{customerId:guid}"), Produces("application/json", Type = typeof(IApplicationResult<bool>))]
        public async Task<IActionResult> Update([FromRoute] Guid customerId, [FromBody] UpdateCustomerCommand command)
        {
            command.CustomerId = customerId;
            return await _mediator.Send(command);
        }

        [HttpDelete("{customerId:guid}"), Produces("application/json", Type = typeof(IApplicationResult<bool>))]
        public async Task<IActionResult> Delete([FromRoute] Guid customerId, [FromQuery] RemoveCustomerCommand command)
        {
            command.CustomerId = customerId;
            return await _mediator.Send(command);
        }
    }
}
