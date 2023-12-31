using Battery_CRM.Core.Domain.Abstract;
using Battery_CRM.Core.Domain.Models.Battery_CRM;
using Battery_CRM.Core.Domain.Models.ViewModels.Customer;
using Battery_CRM.Framework.Extensions;
using Battery_CRM.Infrastructure.Data.Sql.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Battery_CRM.Infrastructure.Data.Sql.Cocrate;

public class CustomerService : ICustomerService
{
    private readonly ApplicationContext _applicationContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CustomerService(ApplicationContext applicationContext, IHttpContextAccessor httpContextAccessor)
    {
        _applicationContext = applicationContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<List<CustomersViewModel>> GetAll()
    {
        HttpContext context = _httpContextAccessor.HttpContext;
        int branchId = int.Parse(context.User.Claims.FirstOrDefault(c => c.Type == "BranchId")?.Value!);

        return _applicationContext.Customers
                                         .AsNoTracking()
                                         .Where(e => e.BranchId == branchId && !e.IsDelete)
                                         .Select(e => new CustomersViewModel
                                         {
                                             Id = e.Id,
                                             BranchId = e.BranchId,
                                             BranchName = e.Branch.Name,
                                             Name = e.Name,
                                             Family = e.Family,
                                             PhoneNumber = e.PhoneNumber
                                         })
                                         .ToListAsync();
    }

    public async Task<CustomerViewModel> Get(int id)
    {
        HttpContext context = _httpContextAccessor.HttpContext;
        int branchId = int.Parse(context.User.Claims.FirstOrDefault(c => c.Type == "BranchId")?.Value!);

        return await Task.Run(() => _applicationContext.Customers
                                                              .AsNoTracking()
                                                              .Where(e => e.Id == id && e.BranchId == branchId && !e.IsDelete)
                                                              .Select(e => new CustomerViewModel
                                                              {
                                                                  Id = e.Id,
                                                                  BranchId = e.BranchId,
                                                                  BranchName = e.Branch.Name,
                                                                  Name = e.Name,
                                                                  Family = e.Family,
                                                                  PhoneNumber = e.PhoneNumber
                                                              })
                                                              .FirstOrDefault()!);
    }

    public Task<Result> Add(CreateCustomerViewModel model)
    {
        try
        {
            var customer = new Customer()
            {
                BranchId = model.BranchId,
                Name = model.Name,
                Family = model.Family,
                PhoneNumber = model.PhoneNumber
            };

            _applicationContext.Customers.Add(customer);
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

    public Task<Result> Update(UpdateCustomerViewModel model)
    {
        try
        {
            HttpContext context = _httpContextAccessor.HttpContext;
            int branchId = int.Parse(context.User.Claims.FirstOrDefault(c => c.Type == "BranchId")?.Value!);

            if (model.BranchId != branchId)
            {
                return Task.Run(() => new Result()
                {
                    Message = "شما مجاز به این عملیات نمیباشید.",
                    Status = false
                });
            }

            var currentCustomer = _applicationContext.Customers.FirstOrDefault(e => !e.IsDelete && e.Id == model.Id);
            if (currentCustomer is null)
            {
                return Task.Run(() => new Result()
                {
                    Message = "مشتری ای با اين شناسه وجود ندارد.",
                    Status = false
                });
            }

            currentCustomer.BranchId = model.BranchId;
            currentCustomer.Name = model.Name;
            currentCustomer.Family = model.Family;
            currentCustomer.PhoneNumber = model.PhoneNumber;

            _applicationContext.Entry(currentCustomer).State = EntityState.Modified;
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
            HttpContext context = _httpContextAccessor.HttpContext;
            int branchId = int.Parse(context.User.Claims.FirstOrDefault(c => c.Type == "BranchId")?.Value!);

            var currentCustomer = _applicationContext.Customers.FirstOrDefault(e => !e.IsDelete && e.Id == id);

            if (currentCustomer is null)
            {
                return Task.Run(() => new Result()
                {
                    Message = "مشتری با اين شناسه وجود ندارد.",
                    Status = false
                });
            }

            if (currentCustomer.BranchId != branchId)
            {
                return Task.Run(() => new Result()
                {
                    Message = "شما مجاز به این عملیات نمیباشید.",
                    Status = false
                });
            }

            currentCustomer.IsDelete = true;
            currentCustomer.DeleteDate = DateTime.Now;

            _applicationContext.Entry(currentCustomer).State = EntityState.Modified;
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

