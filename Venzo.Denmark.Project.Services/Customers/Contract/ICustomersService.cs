using Venzo.Denmark.Project.Services.Models.Base;
using Venzo.Denmark.Project.Services.Models.Venzo;

namespace Venzo.Denmark.Project.Services.Customers.Contract
{
    public interface ICustomersService
    {
        //&&Task<IEnumerable<CustomerModel>> GetCustomersAsync(int skip, int take);
        Task<PagingBase<CustomerModel>> GetCustomersAsync(int skip, int take);
    }
}
