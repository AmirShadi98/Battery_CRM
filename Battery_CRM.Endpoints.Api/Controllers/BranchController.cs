using Battery_CRM.Core.Domain.Abstract;
using Battery_CRM.Core.Domain.Models.ViewModels.Branch;
using Battery_CRM.Framework.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Battery_CRM.Endpoints.Api.Controllers;


[Route("api/[controller]")]
[SwaggerTag("شعبه")]
[ApiController]
[Authorize(Roles = "Admin")]
public class BranchController : BaseController
{
    private readonly IBranchService _branchService;

    public BranchController(IBranchService userService)
    {
        _branchService = userService;
    }

    [HttpGet]
    [SwaggerOperation("همه شعبه ها")]
    [SwaggerResponse(200, "Success", typeof(Result))]
    [SwaggerResponse(404, "Not Found")]
    public async Task<IActionResult> GetAll() => Ok(await _branchService.GetAll());

    [HttpGet, Route("GetById/{id}")]
    [SwaggerOperation("شعبه")]
    [SwaggerResponse(200, "Success", typeof(Result))]
    [SwaggerResponse(404, "Not Found")]
    public async Task<IActionResult> Get(int id) => Ok(await _branchService.Get(id));

    [HttpPost]
    [SwaggerOperation("افزودن شعبه جدید")]
    [SwaggerResponse(200, "Success", typeof(Result))]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(400, "Bad Request")]
    public async Task<IActionResult> Add(CreateBranchViewModel model)
    {
        var result = await _branchService.Add(model);

        if (result.Status)
            return Ok(result.Message);
        else
            return BadRequest(result.Message);
    }

    [HttpPut]
    [SwaggerOperation("ویرایش شعبه")]
    [SwaggerResponse(200, "Success", typeof(Result))]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(400, "Bad Request")]
    public async Task<IActionResult> Update(UpdateBranchViewModel model)
    {
        var result = await _branchService.Update(model);

        if (result.Status)
            return Ok(result.Message);
        else
            return BadRequest(result.Message);
    }

    [HttpDelete]
    [SwaggerOperation("حذف شعبه")]
    [SwaggerResponse(200, "Success", typeof(Result))]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(400, "Bad Request")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _branchService.Delete(id);

        if (result.Status)
            return Ok(result.Message);
        else
            return BadRequest(result.Message);
    }


}
