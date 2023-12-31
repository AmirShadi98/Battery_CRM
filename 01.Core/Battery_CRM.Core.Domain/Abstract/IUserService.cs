using Battery_CRM.Core.Domain.Models.Battery_CRM;
using Battery_CRM.Core.Domain.Models.ViewModels.User;
using Battery_CRM.Framework.Extensions;
using System.Security.Claims;

namespace Battery_CRM.Core.Domain.Abstract;

public interface IUserService
{
    Task<List<UsersViewModel>> GetAll();

    Task<UserViewModel> Get(int id);

    Task<Result> Add(CreateUserViewModel model);

    Task<Result> Update(UpdateUserViewModel model);

    Task<Result> Delete(int id);

    Task<ClaimsPrincipal?> SignIn(AccountViewModel model);
}
