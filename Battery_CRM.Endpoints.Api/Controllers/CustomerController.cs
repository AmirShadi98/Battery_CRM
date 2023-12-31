using Battery_CRM.Core.Domain.Abstract;
using Battery_CRM.Core.Domain.Models.ViewModels.Customer;
using Battery_CRM.Framework.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Battery_CRM.Endpoints.Api.Controllers;

[Route("api/[controller]")]
[SwaggerTag("مشتریان")]
[ApiController]
public class CustomerController : BaseController
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService userService)
    {
        _customerService = userService;
    }

    [HttpGet]
    [SwaggerOperation("همه مشتری ها")]
    [SwaggerResponse(200, "Success", typeof(Result))]
    [SwaggerResponse(404, "Not Found")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll() => Ok(await _customerService.GetAll());

    [HttpGet, Route("GetById/{id}")]
    [SwaggerOperation("مشتری")]
    [SwaggerResponse(200, "Success", typeof(Result))]
    [SwaggerResponse(404, "Not Found")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Get(int id) => Ok(await _customerService.Get(id));

    [HttpPost]
    [SwaggerOperation("افزودن مشتری جدید")]
    [SwaggerResponse(200, "Success", typeof(Result))]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(400, "Bad Request")]
    [Authorize(Roles = "Admin,Operator")]
    public async Task<IActionResult> Add(CreateCustomerViewModel model)
    {
        var result = await _customerService.Add(model);

        if (result.Status)
            return Ok(result.Message);
        else
            return BadRequest(result.Message);
    }

    [HttpPut]
    [SwaggerOperation("ویرایش مشتری")]
    [SwaggerResponse(200, "Success", typeof(Result))]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(400, "Bad Request")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(UpdateCustomerViewModel model)
    {
        var result = await _customerService.Update(model);

        if (result.Status)
            return Ok(result.Message);
        else
            return BadRequest(result.Message);
    }

    [HttpDelete]
    [SwaggerOperation("حذف مشتری")]
    [SwaggerResponse(200, "Success", typeof(Result))]
    [SwaggerResponse(404, "Not Found")]
    [SwaggerResponse(400, "Bad Request")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _customerService.Delete(id);

        if (result.Status)
            return Ok(result.Message);
        else
            return BadRequest(result.Message);
    }


}
