using Venzo.Denmark.Project.Services.Models.Base;

namespace Venzo.Denmark.Project.Services.Models.Venzo
{
    public class VenzoModel : BaseModel<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Employees { get; set; }
    }
}
