using Microsoft.AspNetCore.Mvc;
using Venzo.Denmark.Project.Services.Models.Reservation;
using Venzo.Denmark.Project.Services.Reservation.Contract;

namespace Venzo.Denmark.Project.Web.Api.Controllers
{
    public class ReservationsController : ControllerBase
    {
        private readonly ILogger<ReservationsController> _logger;
        private readonly IReservationsService _reservationService;
        public ReservationsController(ILogger<ReservationsController> logger,
                              IReservationsService reservationService)
        {
            _logger = logger;
            _reservationService = reservationService;
        }

        [HttpPost("AddReservationAsync")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddReservationAsync([FromBody] ReservationModel model)
        {
            try
            {
                var result = await _reservationService.AddAsync(model);

                return Ok(result);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
