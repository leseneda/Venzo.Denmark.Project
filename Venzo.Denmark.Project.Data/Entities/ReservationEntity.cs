using System.ComponentModel.DataAnnotations;
using Venzo.Denmark.Project.Data.Entities.Base;

namespace Venzo.Denmark.Project.Data.Entities
{
    public class ReservationEntity : EntityBase<int>
    {
        [Required] public DateTime DateTo { get; set; }
        [Required] public DateTime DateFrom { get; set; }
        [Required] public int ReservedPeople { get; set; }
        [Required] public int ResourceId { get; set; }
        public virtual RoomEntity Room { get; set; }
    }
}
