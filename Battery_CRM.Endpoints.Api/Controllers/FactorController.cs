using Battery_CRM.Core.Domain.Abstract;
using Battery_CRM.Core.Domain.Models.ViewModels.Factor;
using Battery_CRM.Framework.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Battery_CRM.Endpoints.Api.Controllers;

[Route("api/[controller]")]
[SwaggerTag("فاکتور")]
[ApiController]
public class FactorController : BaseController
{
    private readonly IFactorService _factorService;

    public FactorController(IFactorService userService)
    {
        _factorService = userService;
    }

    [HttpGet]
    [SwaggerOperation("همه فاکتور ها")]
    [SwaggerResponse(200, "Success", typeof(Result))]
    [SwaggerResponse(404, "Not Found")]
    [Authorize(Roles = "Admin,Operator")]
    public async Task<IActionResult> GetAll() => Ok(await _factorService.GetAll());

    [HttpGet, Route("GetById/{id}")]
    [SwaggerOperation("فاکتور")]
    [SwaggerResponse(200, "Success", typeof(Result))]
    [SwaggerResponse(404, "Not Found")]
    [Authorize(Roles = "Admin,Operator")]
    public async Task<IActionResult> Get(int id) => Ok(await _factorService.Get(id));

    [HttpPost]
    [SwaggerOperation("افزودن فاکتور جدید")]
    [SwaggerResponse(200, "Success", typeof(Result))]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(400, "Bad Request")]
    [Authorize(Roles = "Admin,Operator")]
    public async Task<IActionResult> Add(CreateFactorViewModel model)
    {
        var result = await _factorService.Add(model);

        if (result.Status)
            return Ok(result.Message);
        else
            return BadRequest(result.Message);
    }
}