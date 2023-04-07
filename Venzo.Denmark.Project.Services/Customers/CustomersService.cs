using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Net.NetworkInformation;
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

        public async Task<PagingBaseModel<CustomerModel>> GetCustomersAsync(int skip, int take)
        {
            try
            {
                var result = LoadFakeData();

                PagingBaseModel<CustomerModel> pagingResult = new()
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
            foreach (var item in Enumerable.Range(1, 100))
            {
                yield return new CustomerModel()
                {
                    Id = item,
                    Name = $"Company Name {item}",
                    Address = $"Address {item}",
                    City = $"City {item}",
                    Country = $"Country {item}",
                    Region = $"Region {item}",
                    Phone = new Random().Next(0, int.MaxValue).ToString(),
                    HomePage = $"Http://www.homePage{item}.com",
                    PostalCode = new Random().Next(0, int.MaxValue).ToString(),
                };
            }
        }
    }
}
