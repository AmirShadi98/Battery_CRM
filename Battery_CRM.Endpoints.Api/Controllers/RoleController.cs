using Battery_CRM.Core.Domain.Abstract;
using Battery_CRM.Core.Domain.Models.ViewModels.Role;
using Battery_CRM.Framework.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Battery_CRM.Endpoints.Api.Controllers;

[Route("api/[controller]")]
[SwaggerTag("نقش")]
[ApiController]
[Authorize(Roles = "Admin")]
public class RoleController : BaseController
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    [SwaggerOperation("همه نقش ها")]
    [SwaggerResponse(200, "Success", typeof(Result))]
    [SwaggerResponse(404, "Not Found")]
    public async Task<IActionResult> GetAlll() => Ok(await _roleService.GetAll());

    [HttpGet,Route("GetById/{id}")]
    [SwaggerOperation("نقش")]
    [SwaggerResponse(200, "Success", typeof(Result))]
    [SwaggerResponse(404, "Not Found")]
    public async Task<IActionResult> Get(int id) => Ok(await _roleService.Get(id));

    [HttpPost]
    [SwaggerOperation("افزودن نقش جدید")]
    [SwaggerResponse(200, "Success", typeof(Result))]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(400, "Bad Request")]
    public async Task<IActionResult> Add(CreateRoleViewModel model)
    {
        var result = await _roleService.Add(model);

        if (result.Status)
            return Ok(result.Message);
        else
            return BadRequest(result.Message);
    }

    [HttpPut]
    [SwaggerOperation("ویرایش نقش")]
    [SwaggerResponse(200, "Success", typeof(Result))]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(400, "Bad Request")]
    public async Task<IActionResult> Update(UpdateRoleViewModel model)
    {
        var result = await _roleService.Update(model);

        if (result.Status)
            return Ok(result.Message);
        else
            return BadRequest(result.Message);
    }

    [HttpDelete]
    [SwaggerOperation("حذف نقش")]
    [SwaggerResponse(200, "Success", typeof(Result))]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(400, "Bad Request")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _roleService.Delete(id);

        if (result.Status)
            return Ok(result.Message);
        else
            return BadRequest(result.Message);
    }


}
