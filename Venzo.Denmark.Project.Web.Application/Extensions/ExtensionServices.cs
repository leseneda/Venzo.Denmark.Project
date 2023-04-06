using Radzen;
using Venzo.Denmark.Project.Web.Api.Client;

namespace Venzo.Denmark.Project.Web.Application.Extensions
{
    public static class ExtensionServices
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration, string baseAddress)
        {
            services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });
            services.AddSingleton(configuration);

            services.AddScoped<DialogService>()
                    .AddScoped<NotificationService>()
                    .AddScoped<TooltipService>()
                    .AddScoped<ContextMenuService>();

            string baseUrl = configuration.GetSection("Project:Api:BaseUrl").Value;

            services.AddScoped<IVenzoClient, VenzoClient>(factory => new VenzoClient(baseUrl, factory.GetRequiredService<HttpClient>()));
            services.AddScoped<ICustomersClient, CustomersClient>(factory => new CustomersClient(baseUrl, factory.GetRequiredService<HttpClient>()));

            services.AddAutoMapper(config => config.AddMappings(configuration));
        }
    }
}