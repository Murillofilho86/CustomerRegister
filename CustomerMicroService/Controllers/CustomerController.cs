﻿using CustomerMicroService.Application.Commands;
using CustomerMicroService.Application.Queries.Interfaces;
using CustomerMicroService.Application.Requests;
using CustomerMicroService.Data.Services;
using CustomerMicroService.Data.Services.Responses;
using CustomerMicroService.Framework.DomainObject;
using CustomerMicroService.Framework.Result.Interface;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
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
        public async Task<IActionResult> GetByUnidadeId([FromRoute] Guid customerId)
          => await _query.GetByIdAsync(customerId);


        [HttpPost(), Produces("application/json", Type = typeof(IApplicationResult<Guid>))]
        public async Task<IActionResult> PostCatalogo([FromBody] CreateCustomerCommand command)
        {
            //if (command.IsValid())
            //{
            return await _mediator.Send(command);
            //}

            //return BadRequest(command);
        }



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


        [AllowAnonymous]
        [HttpGet(@"find-address/{cep}")]
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
