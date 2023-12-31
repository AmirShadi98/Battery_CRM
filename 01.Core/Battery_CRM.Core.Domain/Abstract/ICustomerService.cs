using Battery_CRM.Core.Domain.Models.ViewModels.Customer;
using Battery_CRM.Core.Domain.Models.ViewModels.Customer;
using Battery_CRM.Framework.Extensions;

namespace Battery_CRM.Core.Domain.Abstract;

public interface ICustomerService
{
    Task<List<CustomersViewModel>> GetAll();

    Task<CustomerViewModel> Get(int id);

    Task<Result> Add(CreateCustomerViewModel model);

    Task<Result> Update(UpdateCustomerViewModel model);

    Task<Result> Delete(int id);
}
