using Microsoft.AspNetCore.Mvc;

namespace Venzo.Denmark.Project.Web.Api.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class VenzoController : Controller
    {

        private readonly ILogger<VenzoController> _logger;

        public VenzoController(ILogger<VenzoController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Company")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCompany()
        {
            try
            {
                return Ok(await Task.FromResult("Venzo Software"));

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}