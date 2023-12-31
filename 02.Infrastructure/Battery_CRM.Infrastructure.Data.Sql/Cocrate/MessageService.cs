using Battery_CRM.Core.Domain.Abstract;
using Battery_CRM.Core.Domain.Models.Battery_CRM;
using Battery_CRM.Core.Domain.Models.ViewModels.Message;
using Battery_CRM.Framework.Extensions;
using Battery_CRM.Infrastructure.Data.Sql.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Battery_CRM.Infrastructure.Data.Sql.Cocrate;

public class MessageService : IMessageService
{
    private readonly ApplicationContext _applicationContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private readonly string _token;
    private readonly string _senderNumber;

    public MessageService(ApplicationContext applicationContext, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
    {
        _applicationContext = applicationContext;
        _httpContextAccessor = httpContextAccessor;
        _httpClient = new HttpClient();
        _baseUrl = configuration.GetSection("BaseUrl").Value!;
        _token = configuration.GetSection("ApiToken").Value!;
        _senderNumber = configuration.GetSection("SenderNumber").Value!;
    }

    public async Task<List<MessagesViewModel>> GetMessages(bool isSend, int? branchId, int? UserId, int pageNumber, int pageSize)
    {
        return await Task.Run(() => _applicationContext.Message
                                         .AsNoTracking()
                                         .Include(e => e.Customer)
                                         .Include(e => e.User)
                                         .ThenInclude(e => e.UserBranches)
                                         .Where(e => e.IsSend == isSend &&
                                                     branchId != null ? e.User.UserBranches.Any(e => e.BranchId == branchId) : true &&
                                                     UserId != null ? e.User.Id == UserId : true)
                                         .Select(e => new MessagesViewModel
                                         {
                                             BranchId = e.BranchId,
                                             BranchName = e.Customer.Branch.Name,
                                             ReciverId = e.Customer.Id,
                                             ReciverName = e.Customer.Name,
                                             SendDate = e.SendDate,
                                             SenderId = e.User.Id,
                                             SenderName = e.User.Name,
                                             Text = e.Text
                                         })
                                         .Take(pageSize)
                                         .Skip(pageNumber)
                                         .ToListAsync());
    }

    public async Task<MessageResult> Send(CreateAndSendMessage model)
    {
        try
        {
            HttpContext context = _httpContextAccessor.HttpContext;

            int currentBranchId = int.Parse(context.User.Claims.FirstOrDefault(c => c.Type == "BranchId")?.Value!);
            int currentUserId = int.Parse(context.User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value!);

            List<CustomesAndNumbers> customerNumbers = _applicationContext.Customers.Where(e => e.BranchId == currentBranchId && !e.IsDelete)
                                                       .Select(e => new CustomesAndNumbers
                                                       {
                                                           PhoneNumber = e.PhoneNumber.ToString(),
                                                           CId = e.Id
                                                       })
                                                       .ToList();

            var result =  await SendSms(customerNumbers.Select(e => e.PhoneNumber)
                                                       .ToList(), _senderNumber, model.Text)!;

            if (result is null)
                return new MessageResult();

            if (result.@return.status != StatusCodes.Status200OK)
            {
                foreach (var item in customerNumbers)
                {
                    var message = new Message()
                    {
                        BranchId = currentBranchId,
                        SendDate = DateTime.Now,
                        SenderId = currentUserId,
                        ReciverId = item.CId,
                        Text = model.Text,
                        IsSend = false
                    };

                    _applicationContext.Message.Add(message);
                    _applicationContext.SaveChanges();
                }

                return result;
            }
            else
            {
                foreach (var item in customerNumbers)
                {
                    var message = new Message()
                    {
                        BranchId = currentBranchId,
                        SendDate = DateTime.Now,
                        SenderId = currentUserId,
                        ReciverId = item.CId,
                        Text = model.Text,
                        IsSend = true
                    };

                    _applicationContext.Message.Add(message);
                    _applicationContext.SaveChanges();
                }

                return result;
            }

        }
        catch (Exception ex)
        {
            return new MessageResult();
        }
    }

    private async Task<MessageResult> SendSms(List<string> numbers, string sender, string message)
    {
        string url = $"{_baseUrl}/{_token}/sms/send.json?receptor={string.Join(",", numbers)}&sender={sender}&message={message}";

        HttpResponseMessage response = await _httpClient.GetAsync(url);

        string jsonResponse = await response.Content.ReadAsStringAsync();

            MessageResult result = JsonSerializer.Deserialize<MessageResult>(jsonResponse)!;
            return result;
    }
}

