using Newtonsoft.Json;
using System.Text.Json;

namespace CustomerMicroService.API.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddSingleton(new JsonSerializerOptions());

         

            return services;
        }

    }
}

