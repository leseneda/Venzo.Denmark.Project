using Venzo.Denmark.Project.Services.Models.Base;
using Venzo.Denmark.Project.Services.Models.Reservation;

namespace Venzo.Denmark.Project.Services.Room.Contract
{
    public interface IRoomsService
    {
        Task<PagingBaseModel<RoomModel>> GetAll(int skip, int take);
        Task<RoomModel> GetById(int id);
    }
}
