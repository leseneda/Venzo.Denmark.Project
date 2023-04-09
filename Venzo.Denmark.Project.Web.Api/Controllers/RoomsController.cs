using Microsoft.AspNetCore.Mvc;
using Venzo.Denmark.Project.Services.Models.Base;
using Venzo.Denmark.Project.Services.Models.Reservation;
using Venzo.Denmark.Project.Services.Room.Contract;

namespace Venzo.Denmark.Project.Web.Api.Controllers
{
    public class RoomsController : ControllerBase
    {
        private readonly ILogger<RoomsController> _logger;
        private readonly IRoomsService _roomService;

        public RoomsController(ILogger<RoomsController> logger,
                               IRoomsService roomService)
        {
            _logger = logger;
            _roomService = roomService;
        }

        [HttpGet("Rooms")]
        [ProducesResponseType(typeof(PagingBaseModel<RoomModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync(int skip, int take)
        {
            try
            {
                var result = await _roomService.GetAll(skip, take);

                return Ok(result != default ? result : null);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("Rooms/{id:int}")]
        [ProducesResponseType(typeof(PagingBaseModel<RoomModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var result = await _roomService.GetById(id);

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
