using AutoMapper;
using Venzo.Denmark.Project.Web.Application.Models;
using client = Venzo.Denmark.Project.Web.Api.Client;

namespace Venzo.Denmark.Project.Web.Application.Extensions
{
    public static class MappingServices
    {
        public static void AddMappings(this IMapperConfigurationExpression mapConfiguration, IConfiguration configuration)
        {
            mapConfiguration.CreateMap<client.VenzoModel, VenzoModel>().ReverseMap();
        }
    }
}
