using AutoMapper;
using CustomerMicroService.Application.Commands;
using CustomerMicroService.Application.Handlers;
using CustomerMicroService.Application.Queries.Concrete;
using CustomerMicroService.Application.Queries.Interfaces;
using CustomerMicroService.Data.Repository;
using CustomerMicroService.Domain.Repository;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerMicroService.API.Configuration
{
    public static class DependenceInjectionConfig
    {
        public static void AddRegisterServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<IMediator, Mediator>();


            services.AddScoped<IRequestHandler<CreateCustomerCommand, IActionResult>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCustomerCommand, IActionResult>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveCustomerCommand, IActionResult>, CustomerCommandHandler>();


            services.AddScoped<ICustomerQuery, CustomerQuery>();

            //services.AddScoped<ICustomerRepository, CustomerRepository>();


        }
    }
}
