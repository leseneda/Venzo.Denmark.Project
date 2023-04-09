using Venzo.Denmark.Project.Services.Models.Base;

namespace Venzo.Denmark.Project.Services.Models.Reservation
{
    public class RoomModel : BaseModel<int>
    {
        public string Name { get; set; }
        public int NumberOfPeople { get; set; }
    }
}
