using Venzo.Denmark.Project.Services.Models.Base;

namespace Venzo.Denmark.Project.Services.Models.Reservation
{
    public class ReservationModel : BaseModel<int>
    {
        public DateTime DateTo { get; set; }
        public DateTime DateFrom { get; set; }
        public int ReservedPeople { get; set; }
        public int ResourceId { get; set; }
    }
}
