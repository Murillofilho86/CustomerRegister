using CustomerMicroService.Application.Commands;
using CustomerMicroService.Application.Handlers;
using CustomerMicroService.Application.Queries.Concrete;
using CustomerMicroService.Application.Queries.Interfaces;
using CustomerMicroService.Data.Context;
using CustomerMicroService.Data.Repository;
using CustomerMicroService.Data.Services;
using CustomerMicroService.Domain.Repository;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Extensions.Http;

namespace CustomerMicroService.API.Configuration
{
    public static class DependenceInjectionConfig
    {
        public static void AddRegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CustomerContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), options =>
            {
                options.MigrationsAssembly(typeof(CustomerContext).Assembly.FullName);

                options.EnableRetryOnFailure(10);
            }));


         
            services.AddTransient<IMediator, Mediator>();


            services.AddScoped<IRequestHandler<CreateCustomerCommand, IActionResult>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCustomerCommand, IActionResult>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveCustomerCommand, IActionResult>, CustomerCommandHandler>();
      

            services.Configure<BrasilApiOptions>(configuration.GetSection("BrasilApi"));


            services.AddHttpClient<IBrasilApiService, BrasilApiService>((serviceProvider, httpClient) =>
            {
                var options = serviceProvider.GetService<IOptions<BrasilApiOptions>>();

                httpClient.BaseAddress = new Uri(options.Value.BaseUrl);
               
            }).SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))));

         

            services.AddScoped<ICustomerQuery, CustomerQuery>();

            services.AddTransient<ICustomerRepository, CustomerRepository>();


        }
    }
}
