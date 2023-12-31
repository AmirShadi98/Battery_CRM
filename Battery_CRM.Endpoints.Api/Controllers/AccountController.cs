using Battery_CRM.Core.Domain.Abstract;
using Battery_CRM.Core.Domain.Models.ViewModels.Role;
using Battery_CRM.Core.Domain.Models.ViewModels.User;
using Battery_CRM.Framework.Extensions;
using Battery_CRM.Infrastructure.Data.Sql.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Battery_CRM.Endpoints.Api.Controllers;

[Route("api/[controller]")]
[SwaggerTag("ورود / خروج کاربر")]
[ApiController]
public class AccountController : BaseController
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("SignIn")]
    [SwaggerOperation("ورود")]
    [SwaggerResponse(200, "Success", typeof(Result))]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(400, "Bad Request")]
    public async Task<IActionResult> SignIn(AccountViewModel model)
    {
        try
        {
            var userPrincipal = await _userService.SignIn(model);

            if (userPrincipal is null)
                return BadRequest(ModelState);

            await HttpContext.SignInAsync(userPrincipal);

            return Content("خوش آمديد");
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    [Authorize]

    [HttpGet("Signout")]
    [SwaggerOperation("خروج")]
    public async Task<IActionResult> Signout()
    {
        await HttpContext.SignOutAsync();

        return Content("خدا نگهدارتان");
    }


    [HttpGet("AccessDenied")]
    [SwaggerOperation("دسترسی")]
    public IActionResult AccessDenied()
    {
        return Content("شما به اين بخش دسترسي نداريد");
    }
}
