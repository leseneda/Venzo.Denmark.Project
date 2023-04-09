using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Venzo.Denmark.Project.Data.Contexts;
using Venzo.Denmark.Project.Data.Entities;
using Venzo.Denmark.Project.Services.Models.Base;
using Venzo.Denmark.Project.Services.Models.Reservation;
using Venzo.Denmark.Project.Services.Room.Contract;

namespace Venzo.Denmark.Project.Services.Room
{
    public class RoomsService : IRoomsService
    {
        readonly ILogger<RoomsService> _logger;
        readonly IMapper _mapper;
        protected readonly ProjectContext _dbContext;
        
        public RoomsService(ILogger<RoomsService> logger,
                            IMapper mapper,
                            ProjectContext dbContext)
        {
            _logger = logger;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<PagingBaseModel<RoomModel>> GetAll(int skip, int take)
        {
            try
            {
                var results = await _dbContext.Set<RoomEntity>().AsNoTracking()
                                                                .Skip(skip)
                                                                .Take(take)
                                                                .ToListAsync();

                var rooms = _dbContext.Set<RoomEntity>();
                var reservations = _dbContext.Set<ReservationEntity>();

                if (!results.Any())
                {
                    return default;
                }

                var model = _mapper.Map<IEnumerable<RoomModel>>(results);

                return new PagingBaseModel<RoomModel>()
                {
                    Count = model.Count(),
                    Items = model.AsEnumerable()
                };
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

        public async Task<RoomModel> GetById(int id)
        {
            try
            {
                var results = await _dbContext.Set<RoomEntity>().SingleAsync(room => room.Id == id);

                return results == default ? default : _mapper.Map<RoomModel>(results);
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
