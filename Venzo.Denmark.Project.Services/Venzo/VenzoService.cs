using AutoMapper;
using Microsoft.Extensions.Logging;
using Venzo.Denmark.Project.Services.Models.Venzo;
using Venzo.Denmark.Project.Services.Venzo.Contract;

namespace Venzo.Denmark.Project.Services.Venzo
{
    public class VenzoService : IVenzoService
    {
        readonly ILogger<VenzoService> _logger;
        readonly IMapper _mapper;

        public VenzoService(ILogger<VenzoService> logger,
                            IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<VenzoModel> GetCompanyAsync()
        {
            try
            {
                var model = new VenzoModel()
                {
                    Id = 1,
                    Name = "Venzo Software",
                    Description = "One of the most greatest software company in the world.",
                    Employees = 5000,
                };

                return await Task.FromResult(_mapper.Map<VenzoModel>(model));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                throw;
            }
        }
    }
}
