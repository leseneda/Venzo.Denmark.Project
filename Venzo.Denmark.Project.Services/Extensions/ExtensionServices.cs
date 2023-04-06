using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Venzo.Denmark.Project.Services.Customers;
using Venzo.Denmark.Project.Services.Customers.Contract;
using Venzo.Denmark.Project.Services.Models.Venzo;
using Venzo.Denmark.Project.Services.Venzo;
using Venzo.Denmark.Project.Services.Venzo.Contract;

namespace Venzo.Denmark.Project.Services.Extensions
{
    public static class ExtensionServices
    {
        public static void AddProjectServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Logger.
            services.AddSingleton<ILogger, Logger<VenzoService>>();

            // Add AutoMapper
            services.AddAutoMapper(config => config.AddProjectMappings(configuration));

            // Services
            services.AddScoped<IVenzoService, VenzoService>();
            services.AddScoped<ICustomersService, CustomersService>();
        }

        public static void AddProjectMappings(this IMapperConfigurationExpression config, IConfiguration configuration)
        {
            // Just for example
            config.CreateMap<VenzoModel, VenzoModel>().ReverseMap();
            config.CreateMap<CustomerModel, CustomerModel>().ReverseMap();
        }
    }
}