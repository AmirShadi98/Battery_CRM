using Battery_CRM.Core.Domain.Models.ViewModels.Role;
using Battery_CRM.Framework.Extensions;

namespace Battery_CRM.Core.Domain.Abstract;

public interface IRoleService
{
    Task<List<RolesViewModel>> GetAll();

    Task<RoleViewModel> Get(int id);

    Task<Result> Add(CreateRoleViewModel model);

    Task<Result> Update(UpdateRoleViewModel model);

    Task<Result> Delete(int id);
}
