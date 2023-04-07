using Venzo.Denmark.Project.Services.Models.Base;
using Venzo.Denmark.Project.Services.Models.Venzo;

namespace Venzo.Denmark.Project.Services.Customers.Contract
{
    public interface ICustomersService
    {
        Task<PagingBaseModel<CustomerModel>> GetCustomersAsync(int skip, int take);
    }
}
