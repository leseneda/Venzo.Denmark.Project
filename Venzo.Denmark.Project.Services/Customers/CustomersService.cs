using AutoMapper;
using Microsoft.Extensions.Logging;
using Venzo.Denmark.Project.Services.Customers.Contract;
using Venzo.Denmark.Project.Services.Models.Base;
using Venzo.Denmark.Project.Services.Models.Venzo;
using Venzo.Denmark.Project.Services.Venzo;

namespace Venzo.Denmark.Project.Services.Customers
{
    public class CustomersService : ICustomersService
    {
        readonly ILogger<VenzoService> _logger;
        readonly IMapper _mapper;

        public CustomersService(ILogger<VenzoService> logger,
                                IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PagingBase<CustomerModel>> GetCustomersAsync(int skip, int take)
        {
            try
            {
                var result = LoadFakeData();

                PagingBase<CustomerModel> pagingResult = new()
                {
                    Count = result.Count(),
                    Items = result.Skip(skip)
                                  .Take(take)
                };
                
                return await Task.FromResult(pagingResult);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                throw;
            }
        }

        private static IEnumerable<CustomerModel> LoadFakeData()
        {
            var listCount = Enumerable.Range(1, 100);

            foreach (var item in listCount)
            {
                yield return new CustomerModel()
                {
                    Id = item,
                    Name = $"Company Name {item}",
                    Address = $"Address {item}",
                    City = $"City {item}",
                    Country = $"Country {item}",
                    Region = $"Region {item}",
                    Phone = new Random().Next(int.MinValue, int.MaxValue).ToString(),
                    HomePage = $"www.homePage{item}.com",
                    PostalCode = new Random().Next(int.MinValue, int.MaxValue).ToString(),
                };
            }
        }
    }
}
