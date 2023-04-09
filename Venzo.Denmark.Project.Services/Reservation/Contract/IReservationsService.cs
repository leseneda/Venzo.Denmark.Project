using Venzo.Denmark.Project.Services.Models.Reservation;

namespace Venzo.Denmark.Project.Services.Reservation.Contract
{
    public interface IReservationsService
    {
        Task<bool> AddAsync(ReservationModel model);
    }
}
