using System.ComponentModel.DataAnnotations;
using Venzo.Denmark.Project.Data.Entities.Base;

namespace Venzo.Denmark.Project.Data.Entities
{
    public class RoomEntity : EntityBase<int>
    {
        [Required] public string Name { get; set; }
        [Required] public int NumberOfPeople { get; set; }
        public virtual IList<ReservationEntity> Reservations { get; set; }
    }
}
