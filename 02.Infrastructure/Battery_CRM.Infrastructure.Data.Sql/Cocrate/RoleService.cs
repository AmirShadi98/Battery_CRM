using Battery_CRM.Core.Domain.Abstract;
using Battery_CRM.Core.Domain.Models.Battery_CRM;
using Battery_CRM.Core.Domain.Models.ViewModels.Role;
using Battery_CRM.Framework.Extensions;
using Battery_CRM.Infrastructure.Data.Sql.Common;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Battery_CRM.Infrastructure.Data.Sql.Cocrate;

public class RoleService : IRoleService
{
    private readonly ApplicationContext _applicationContext;

    public RoleService(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public Task<List<RolesViewModel>> GetAll() => _applicationContext.Roles
                                                                           .AsNoTracking()
                                                                           .Select(e => new RolesViewModel
                                                                           {
                                                                               Id = e.Id,
                                                                               Title = e.Title
                                                                           })
                                                                           .ToListAsync();

    public async Task<RoleViewModel> Get(int id) => await Task.Run(() => _applicationContext.Roles
                                                                           .AsNoTracking()
                                                                           .Where(e => e.Id == id)
                                                                           .Select(e => new RoleViewModel
                                                                           {
                                                                               Id = e.Id,
                                                                               Title = e.Title
                                                                           })
                                                                           .FirstOrDefault()!);

    public Task<Result> Add(CreateRoleViewModel model)
    {
        try
        {
            var role = new Role()
            {
                Title = model.Title
            };

            _applicationContext.Roles.Add(role);
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

    public Task<Result> Update(UpdateRoleViewModel model)
    {
        try
        {
            var currentRole = _applicationContext.Roles.FirstOrDefault(e => e.Id == model.Id);
            if (currentRole is null)
            {
                return Task.Run(() => new Result()
                {
                    Message = "نقشي با اين شناسه وجود ندارد.",
                    Status = false
                });
            }

            currentRole.Title = model.Title;
            
            _applicationContext.Entry(currentRole).State = EntityState.Modified;
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
            var currentRole = _applicationContext.Roles.FirstOrDefault(e => e.Id == id);

            if (currentRole is null)
            {
                return Task.Run(() => new Result()
                {
                    Message = "كاربري با اين شناسه وجود ندارد.",
                    Status = false
                });
            }

            _applicationContext.Entry(currentRole).State = EntityState.Deleted;
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
}
