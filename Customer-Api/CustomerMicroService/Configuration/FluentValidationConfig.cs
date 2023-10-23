using FluentValidation.AspNetCore;
using FluentValidation;
using CustomerMicroService.Application.Commands;

namespace CustomerMicroService.API.Configuration
{
    public static class FluentValidationConfig
    {

        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<CreateCustomerValidation>();

            return services;
        }
    }
}
