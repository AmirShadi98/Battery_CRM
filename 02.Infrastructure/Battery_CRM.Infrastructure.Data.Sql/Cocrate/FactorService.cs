using Battery_CRM.Core.Domain.Abstract;
using Battery_CRM.Core.Domain.Models.Battery_CRM;
using Battery_CRM.Core.Domain.Models.ViewModels.Factor;
using Battery_CRM.Framework.Extensions;
using Battery_CRM.Infrastructure.Data.Sql.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Battery_CRM.Infrastructure.Data.Sql.Cocrate;

public class FactorService : IFactorService
{
    private readonly ApplicationContext _applicationContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FactorService(ApplicationContext applicationContext, IHttpContextAccessor httpContextAccessor)
    {
        _applicationContext = applicationContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<List<FactorsViewModel>> GetAll()
    {
        HttpContext context = _httpContextAccessor.HttpContext;
        int branchId = int.Parse(context.User.Claims.FirstOrDefault(c => c.Type == "BranchId")?.Value!);

        return _applicationContext.Factors
                                         .AsNoTracking()
                                         .Include(e => e.Customer)
                                         .Include(e => e.User)
                                         .ThenInclude(e => e.UserBranches)
                                         .Where(e => e.User.UserBranches.Any(e => e.BranchId == branchId))
                                         .Select(e => new FactorsViewModel
                                         {
                                             Id = e.Id,
                                             BranchId = branchId,
                                             BranchName = e.Customer.Branch.Name,
                                             CustomerId = e.CustomerId,
                                             CustomerName = e.Customer.Name,
                                             UserId = e.User.Id,
                                             UserName = e.User.Name,
                                             FactorCode = e.FactorCode,
                                             Price = e.Price,
                                             ProductName = e.ProductName,
                                             SaleDate = e.SaleDate
                                         })
                                         .ToListAsync();
    }

    public async Task<FactorViewModel> Get(int id)
    {
        HttpContext context = _httpContextAccessor.HttpContext;
        int branchId = int.Parse(context.User.Claims.FirstOrDefault(c => c.Type == "BranchId")?.Value!);

        return await Task.Run(() => _applicationContext.Factors
                                                              .AsNoTracking()
                                                              .Include(e => e.Customer)
                                                              .Include(e => e.User)
                                                              .ThenInclude(e => e.UserBranches)
                                                              .Where(e => e.User.UserBranches.Any(e => e.BranchId == branchId))
                                                              .Select(e => new FactorViewModel
                                                              {
                                                                  Id = e.Id,
                                                                  BranchId = branchId,
                                                                  BranchName = e.Customer.Branch.Name,
                                                                  CustomerId = e.CustomerId,
                                                                  CustomerName = e.Customer.Name,
                                                                  UserId = e.User.Id,
                                                                  UserName = e.User.Name,
                                                                  FactorCode = e.FactorCode,
                                                                  Price = e.Price,
                                                                  ProductName = e.ProductName,
                                                                  SaleDate = e.SaleDate
                                                              })
                                                              .FirstOrDefault()!);
    }

    public Task<Result> Add(CreateFactorViewModel model)
    {
        try
        {
            HttpContext context = _httpContextAccessor.HttpContext;
            int Id = int.Parse(context.User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value!);

            var factor = new Factor()
            {
                CustomerId = model.CustomerId,
                UserId = Id,
                Price = model.Price,
                ProductName = model.ProductName,
                SaleDate = model.SaleDate,
                FactorCode = new Random().Next(1111111,9999999)
            };

            _applicationContext.Factors.Add(factor);
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

