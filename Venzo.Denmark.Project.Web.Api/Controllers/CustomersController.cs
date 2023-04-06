using Microsoft.AspNetCore.Mvc;
using Venzo.Denmark.Project.Services.Customers.Contract;
using Venzo.Denmark.Project.Services.Models.Base;
using Venzo.Denmark.Project.Services.Models.Venzo;

namespace Venzo.Denmark.Project.Web.Api.Controllers
{
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly ICustomersService _customersService;

        public CustomersController(ILogger<CustomersController> logger,
                                   ICustomersService customersService)
        {
            _logger = logger;
            _customersService = customersService;
        }

        [HttpGet("Customers")]
        [ProducesResponseType(typeof(PagingBase<CustomerModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCustomers(int skip, int take)
        {
            try
            {
                var result = await _customersService.GetCustomersAsync(skip, take);

                return Ok(result != default ? result : null);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
