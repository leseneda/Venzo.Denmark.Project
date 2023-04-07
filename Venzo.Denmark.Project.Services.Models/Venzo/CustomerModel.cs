using Venzo.Denmark.Project.Services.Models.Base;

namespace Venzo.Denmark.Project.Services.Models.Venzo
{
    public class CustomerModel : BaseModel<int>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string HomePage { get; set; }
    }
}
