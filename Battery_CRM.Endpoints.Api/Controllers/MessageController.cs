using Battery_CRM.Core.Domain.Abstract;
using Battery_CRM.Core.Domain.Models.Battery_CRM;
using Battery_CRM.Core.Domain.Models.ViewModels.Message;
using Battery_CRM.Framework.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Battery_CRM.Endpoints.Api.Controllers;

[Route("api/[controller]")]
[SwaggerTag("ارسال پیام")]
[ApiController]
public class MessageController : BaseController
{
    private readonly IMessageService _messageService;

    public MessageController(IMessageService userService)
    {
        _messageService = userService;
    }

    [HttpGet]
    [SwaggerOperation("گزارش پیام ها")]
    [SwaggerResponse(200, "Success", typeof(Result))]
    [SwaggerResponse(404, "Not Found")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetMessages(bool isSend, int? branchId, int? UserId, int pageNumber = 0, int pageSize = 10) =>
                                        Ok(await _messageService.GetMessages(isSend, branchId, UserId, pageNumber, pageSize));

    [HttpPost]
    [SwaggerOperation("ارسال پیام به مشتریان")]
    [SwaggerResponse(200, "Success", typeof(Result))]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(400, "Bad Request")]
    [Authorize(Roles = "Admin,Operator")]
    public async Task<IActionResult> SendSMS(CreateAndSendMessage model)
    {
        var result = await _messageService.Send(model);

        if (result is not null)
            return Ok(result);
        else
            return BadRequest(result);
    }
}
