using Microsoft.AspNetCore.Mvc;
using Venzo.Denmark.Project.Services.Models.Venzo;
using Venzo.Denmark.Project.Services.Venzo.Contract;

namespace Venzo.Denmark.Project.Web.Api.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class VenzoController : ControllerBase
    {

        private readonly ILogger<VenzoController> _logger;
        private readonly IVenzoService _venzoService;
        public VenzoController(ILogger<VenzoController> logger,
                               IVenzoService venzoService)
        {
            _logger = logger;
            _venzoService = venzoService;
        }

        [HttpGet("Company")]
        [ProducesResponseType(typeof(VenzoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCompany()
        {
            try
            {
                var result = await _venzoService.GetCompanyAsync();

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