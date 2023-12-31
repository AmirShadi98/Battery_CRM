using Battery_CRM.Core.Domain.Abstract;
using Battery_CRM.Core.Domain.Models.Battery_CRM;
using Battery_CRM.Core.Domain.Models.ViewModels.User;
using Battery_CRM.Framework.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Battery_CRM.Endpoints.Api.Controllers;

[Route("api/[controller]")]
[SwaggerTag("کاربران")]
[ApiController]
[Authorize(Roles = "Admin")]
public class UserController : BaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [SwaggerOperation("همه کاربران")]
    [SwaggerResponse(200, "Success", typeof(Result))]
    [SwaggerResponse(404, "Not Found")]
    public async Task<IActionResult> GetAll() => Ok(await _userService.GetAll());

    [HttpGet,Route("GetById/{id}")]
    [SwaggerOperation("کاربر")]
    [SwaggerResponse(200, "Success", typeof(Result))]
    [SwaggerResponse(404, "Not Found")]
    public async Task<IActionResult> Get(int id) => Ok(await _userService.Get(id));

    [HttpPost]
    [SwaggerOperation("افزودن یک کاربر جدید")]
    [SwaggerResponse(200, "Success", typeof(Result))]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(400, "Bad Request")]
    public async Task<IActionResult> Add(CreateUserViewModel model)
    {
        var result = await _userService.Add(model);

        if (result.Status)
            return Ok(result.Message);
        else
            return BadRequest(result.Message);
    }

    [HttpPut]
    [SwaggerOperation("ویرایش کاربر")]
    [SwaggerResponse(200, "Success", typeof(Result))]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(400, "Bad Request")]
    public async Task<IActionResult> Update(UpdateUserViewModel model)
    {
        var result = await _userService.Update(model);

        if (result.Status)
            return Ok(result.Message);
        else
            return BadRequest(result.Message);
    }

    [HttpDelete]
    [SwaggerOperation("حذف کاربر")]
    [SwaggerResponse(200, "Success", typeof(Result))]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(400, "Bad Request")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _userService.Delete(id);

        if (result.Status)
            return Ok(result.Message);
        else
            return BadRequest(result.Message);
    }


}
