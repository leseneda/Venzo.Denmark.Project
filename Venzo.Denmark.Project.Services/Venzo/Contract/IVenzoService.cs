using Venzo.Denmark.Project.Services.Models.Venzo;

namespace Venzo.Denmark.Project.Services.Venzo.Contract
{
    public interface IVenzoService
    {
        Task<VenzoModel> GetCompanyAsync();
    }
}
