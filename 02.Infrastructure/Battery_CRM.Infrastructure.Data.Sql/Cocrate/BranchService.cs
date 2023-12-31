using Battery_CRM.Core.Domain.Abstract;
using Battery_CRM.Core.Domain.Models.Battery_CRM;
using Battery_CRM.Core.Domain.Models.ViewModels.Branch;
using Battery_CRM.Framework.Extensions;
using Battery_CRM.Infrastructure.Data.Sql.Common;
using Microsoft.EntityFrameworkCore;
using System.Security;

namespace Battery_CRM.Infrastructure.Data.Sql.Cocrate;

public class BranchService : IBranchService
{
    private readonly ApplicationContext _applicationContext;

    public BranchService(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public Task<List<BranchesViewModel>> GetAll() => _applicationContext.Branches
                                                                           .AsNoTracking()
                                                                           .Where(e => !e.IsDelete)
                                                                           .Select(e => new BranchesViewModel
                                                                           {
                                                                               Id = e.Id,
                                                                               Address = e.Address,
                                                                               Name = e.Name,
                                                                               PhoneNumber = e.PhoneNumber
                                                                           })
                                                                           .ToListAsync();

    public async Task<BranchViewModel> Get(int id) => await Task.Run(() => _applicationContext.Branches
                                                                           .AsNoTracking()
                                                                           .Where(e => !e.IsDelete && e.Id == id)
                                                                           .Select(e => new BranchViewModel
                                                                           {
                                                                               Id = e.Id,
                                                                               Address = e.Address,
                                                                               Name = e.Name,
                                                                               PhoneNumber = e.PhoneNumber
                                                                           })
                                                                           .FirstOrDefault()!);

    public Task<Result> Add(CreateBranchViewModel model)
    {
        try
        {
            var branch = new Branch()
            {
                Address = model.Address,
                Name = model.Name,
                PhoneNumber = model.PhoneNumber
            };

            _applicationContext.Branches.Add(branch);
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

    public Task<Result> Update(UpdateBranchViewModel model)
    {
        try
        {
            var currentBranch = _applicationContext.Branches.FirstOrDefault(e =>!e.IsDelete && e.Id == model.Id);
            if (currentBranch is null)
            {
                return Task.Run(() => new Result()
                {
                    Message = "شعبه ای با اين شناسه وجود ندارد.",
                    Status = false
                });
            }

            currentBranch.Name = model.Name;
            currentBranch.PhoneNumber = model.PhoneNumber;
            currentBranch.Address = model.Address;

            _applicationContext.Entry(currentBranch).State = EntityState.Modified;
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
            var currentBranch = _applicationContext.Branches.FirstOrDefault(e =>!e.IsDelete && e.Id == id);

            if (currentBranch is null)
            {
                return Task.Run(() => new Result()
                {
                    Message = "كاربري با اين شناسه وجود ندارد.",
                    Status = false
                });
            }

            currentBranch.IsDelete = true;
            currentBranch.DeleteDate = DateTime.Now;

            _applicationContext.Entry(currentBranch).State = EntityState.Modified;
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
