using Battery_CRM.Core.Domain.Abstract;
using Battery_CRM.Core.Domain.Models.Battery_CRM;
using Battery_CRM.Core.Domain.Models.ViewModels.User;
using Battery_CRM.Framework.Extensions;
using Battery_CRM.Infrastructure.Data.Sql.Common;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Battery_CRM.Infrastructure.Data.Sql.Cocrate;

public class UserService : IUserService
{
    private readonly ApplicationContext _applicationContext;

    public UserService(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public Task<List<UsersViewModel>> GetAll() => _applicationContext.Users.AsNoTracking()
                                                                           .Where(e => !e.IsDelete)
                                                                           .Include(e => e.UserBranches)
                                                                           .Select(e => new UsersViewModel
                                                                           {
                                                                               Id = e.Id,
                                                                               Family = e.Family,
                                                                               Name = e.Name,
                                                                               PhoneNumber = e.PhoneNumber,
                                                                               RoleName = e.Role.Title,
                                                                               RoleId = e.Role.Id,
                                                                               BranchName = e.UserBranches
                                                                               .FirstOrDefault(a => a.UserId == e.Id)!.Branch.Name,
                                                                               BranchId = e.UserBranches
                                                                               .FirstOrDefault(a => a.UserId == e.Id)!.Branch.Id
                                                                           })
                                                                           .ToListAsync();

    public async Task<UserViewModel> Get(int id) => await Task.Run(() => _applicationContext.Users
                                                                           .AsNoTracking()
                                                                           .Where(e => !e.IsDelete && e.Id == id)
                                                                           .Select(e => new UserViewModel
                                                                           {
                                                                               Id = e.Id,
                                                                               Family = e.Family,
                                                                               Name = e.Name,
                                                                               PhoneNumber = e.PhoneNumber,
                                                                               RoleName = e.Role.Title,
                                                                               RoleId = e.Role.Id,
                                                                               BranchName = e.UserBranches
                                                                               .FirstOrDefault(a => a.UserId == e.Id)!.Branch.Name,
                                                                               BranchId = e.UserBranches
                                                                               .FirstOrDefault(a => a.UserId == e.Id)!.Branch.Id
                                                                           })
                                                                           .FirstOrDefault()!);

    public Task<Result> Add(CreateUserViewModel model)
    {
        try
        {
            MD5 encriptor = MD5.Create();
            byte[] buffer = encriptor.ComputeHash(Encoding.UTF8.GetBytes(model.Password));
            model.Password = Convert.ToBase64String(buffer);

            var user = new User()
            {
                CreateDate = DateTime.Now,
                Family = model.Family,
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
                UserName = model.UserName,
                Password = model.Password,
                RoleId = model.RoleId
            };

            _applicationContext.Users.Add(user);
            _applicationContext.SaveChanges();

            var userBranch = new UserBranch()
            {
                BranchId = model.BranchId,
                UserId = user.Id,
            };

            _applicationContext.UserBranchs.Add(userBranch);
            _applicationContext.SaveChanges();

            return Task.Run(() => new Result()
            {
                Message = "عمليات با موفقيت انجام شد.",
                Status = true
            });
        }
        catch (Exception ex)
        {
            return Task.Run(() => new Result()
            {
                Message = "مشكلي در نرم افزار بوجود آمده است.",
                Status = false
            });
        }
    }

    public Task<Result> Update(UpdateUserViewModel model)
    {
        try
        {
            var currentUser = _applicationContext.Users.Include(e => e.UserBranches)
                                                       .FirstOrDefault(e => !e.IsDelete && e.Id == model.Id);
            if (currentUser is null)
            {
                return Task.Run(() => new Result()
                {
                    Message = "كاربري با اين شناسه وجود ندارد.",
                    Status = false
                });
            }

            MD5 encriptor = MD5.Create();
            byte[] buffer = encriptor.ComputeHash(Encoding.UTF8.GetBytes(model.Password));

            currentUser.UpdateDate = DateTime.Now;
            currentUser.Family = model.Family;
            currentUser.Name = model.Name;
            currentUser.PhoneNumber = model.PhoneNumber;
            currentUser.UserName = model.UserName;
            currentUser.Password = Convert.ToBase64String(buffer);
            currentUser.RoleId = model.RoleId;
            currentUser.UserBranches.FirstOrDefault()!.BranchId = model.BranchId;

            _applicationContext.Entry(currentUser).State = EntityState.Modified;
            _applicationContext.SaveChanges();

            return Task.Run(() => new Result()
            {
                Message = "عمليات با موفقيت انجام شد.",
                Status = true
            });
        }
        catch (Exception ex)
        {
            return Task.Run(() => new Result()
            {
                Message = "مشكلي در نرم افزار بوجود آمده است.",
                Status = false
            });
        }
    }

    public Task<Result> Delete(int id)
    {
        try
        {
            var currentUser = _applicationContext.Users.FirstOrDefault(e => !e.IsDelete && e.Id == id);

            if (currentUser is null)
            {
                return Task.Run(() => new Result()
                {
                    Message = "كاربري با اين شناسه وجود ندارد.",
                    Status = false
                });
            }

            currentUser.IsDelete = true;
            currentUser.DeleteDate = DateTime.Now;

            _applicationContext.Entry(currentUser).State = EntityState.Modified;
            _applicationContext.SaveChanges();

            return Task.Run(() => new Result()
            {
                Message = "عمليات با موفقيت انجام شد.",
                Status = true
            });
        }
        catch (Exception ex)
        {
            return Task.Run(() => new Result()
            {
                Message = "مشكلي در نرم افزار بوجود آمده است.",
                Status = false
            });
        }
    }

    public async Task<ClaimsPrincipal?> SignIn(AccountViewModel model)
    {
        MD5 encriptor = MD5.Create();
        byte[] buffer = encriptor.ComputeHash(Encoding.UTF8.GetBytes(model.Password));
        model.Password = Convert.ToBase64String(buffer);

        User user = _applicationContext.Users
                                        .Include(e=>e.UserBranches)
                                        .FirstOrDefault(user => user.UserName == model.UserName &&
                                                                user.Password == model.Password && !user.IsDelete)!;
        if (user is null)
        {
            return null;
        }
        string branchId = user?.UserBranches.FirstOrDefault(e => e.UserId == user.Id)?.BranchId.ToString()!;

        List<Claim> claims = new(){
                new Claim(ClaimTypes.NameIdentifier,user.UserName),
                new Claim(ClaimTypes.Name,user.Name + " " + user.Family),
                new Claim(ClaimTypes.CookiePath,"Auth"),
                new Claim(nameof(user.RoleId), user?.RoleId.ToString()!),
                new Claim(nameof(user.Id), user?.Id.ToString()!),
                new Claim(ClaimTypes.Role, user?.UserRoleEnum.ToString()!),
                new Claim("BranchId", branchId)
            };

        var claimIdentity = new ClaimsIdentity(claims, "Basic");
        var userPrincipal = new ClaimsPrincipal(new[] { claimIdentity });

        return await Task.Run(() => userPrincipal);
    }
}
