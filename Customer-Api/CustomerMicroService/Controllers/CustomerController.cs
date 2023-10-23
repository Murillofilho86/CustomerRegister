using CustomerMicroService.Application.Commands;
using CustomerMicroService.Application.Queries.Interfaces;
using CustomerMicroService.Application.Requests;
using CustomerMicroService.Data.Services;
using CustomerMicroService.Data.Services.Responses;
using CustomerMicroService.Framework.Result.Interface;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
        public async Task<IActionResult> GetById([FromRoute] Guid customerId)
          => await _query.GetByIdAsync(customerId);


        [HttpPost(), Produces("application/json", Type = typeof(IApplicationResult<Guid>))]
        public async Task<IActionResult> Post([FromBody] CreateCustomerCommand command)
        {
            if (!command.IsValid())
            {
                return BadRequest(command. ValidationResult.Errors.Select(e => e.ErrorMessage));
            }

            return Ok(await _mediator.Send(command));
        }
            

        [HttpPut("{customerId:guid}"), Produces("application/json", Type = typeof(IApplicationResult<bool>))]
        public async Task<IActionResult> Update([FromRoute] Guid customerId, [FromBody] UpdateCustomerCommand command)
        {
            command.CustomerId = customerId;
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{customerId:guid}"), Produces("application/json", Type = typeof(IApplicationResult<bool>))]
        public async Task<IActionResult> Delete([FromRoute] Guid customerId, RemoveCustomerCommand command)
        {
            command.CustomerId = customerId;
            return Ok(await _mediator.Send(command));
        }


        
        [HttpGet(@"Address/{cep}")]
        public async Task<ActionResult<DocumentInfo>> GetCompanyInfoAsync([FromServices] IBrasilApiService brasilApiService, string cep)
        {

            try
            {
                return await brasilApiService.GetDocumentInfoAsync(cep);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.BadRequest)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            ModelState.AddModelError(string.Empty, "cep inválido");
            return BadRequest(ModelState);
        }
    }
}
