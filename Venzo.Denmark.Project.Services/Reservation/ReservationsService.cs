using AutoMapper;
using Microsoft.Extensions.Logging;
using Venzo.Denmark.Project.Data.Contexts;
using Venzo.Denmark.Project.Data.Entities;
using Venzo.Denmark.Project.Services.Models.Reservation;
using Venzo.Denmark.Project.Services.Reservation.Contract;

namespace Venzo.Denmark.Project.Services.Reservation
{
    public class ReservationsService : IReservationsService
    {
        readonly ILogger<ReservationsService> _logger;
        readonly IMapper _mapper;
        protected readonly ProjectContext _dbContext;

        public ReservationsService(ILogger<ReservationsService> logger,
                                    IMapper mapper,
                                    ProjectContext dbContext)
        {
            _logger = logger;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<bool> AddAsync(ReservationModel model)
        {
            try
            {
                var entity = _mapper.Map<ReservationEntity>(model);
                
                await _dbContext.Set<ReservationEntity>().AddAsync(entity);
                
                var result = await _dbContext.SaveChangesAsync();

                return result > 0;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);

                throw;
            }
            finally
            {
                _dbContext.Dispose();
            }
        }
    }
}
