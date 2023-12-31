using Battery_CRM.Core.Domain.Models.ViewModels.Factor;
using Battery_CRM.Core.Domain.Models.ViewModels.Message;
using Battery_CRM.Framework.Extensions;

namespace Battery_CRM.Core.Domain.Abstract;

public interface IMessageService
{
    Task<List<MessagesViewModel>> GetMessages(bool isSend, int? branchId, int? UserId, int pageNumber, int pageSize);

    Task<MessageResult> Send(CreateAndSendMessage model);
}
