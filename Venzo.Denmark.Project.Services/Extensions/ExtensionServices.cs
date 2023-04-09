using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Venzo.Denmark.Project.Data.Contexts;
using Venzo.Denmark.Project.Data.Entities;
using Venzo.Denmark.Project.Services.Models.Reservation;
using Venzo.Denmark.Project.Services.Reservation;
using Venzo.Denmark.Project.Services.Reservation.Contract;
using Venzo.Denmark.Project.Services.Room;
using Venzo.Denmark.Project.Services.Room.Contract;

namespace Venzo.Denmark.Project.Services.Extensions
{
    public static class ExtensionServices
    {
        public static void AddProjectServices(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetRequiredSection("ConnectionString:Project").Get<string>();

            services.AddDbContext<ProjectContext>(options => options.UseSqlServer(connectionString));

            // Logger.
            services.AddSingleton<ILogger, Logger<RoomsService>>();
            services.AddSingleton<ILogger, Logger<ReservationsService>>();

            // Add AutoMapper
            services.AddAutoMapper(config => config.AddProjectMappings(configuration));

            // Services
            services.AddScoped<IRoomsService, RoomsService>();
            services.AddScoped<IReservationsService, ReservationsService>();
        }

        public static void AddProjectMappings(this IMapperConfigurationExpression config, IConfiguration configuration)
        {
            config.CreateMap<RoomEntity, RoomModel>().ReverseMap();
            config.CreateMap<ReservationEntity, ReservationModel>().ReverseMap();
        }
    }
}