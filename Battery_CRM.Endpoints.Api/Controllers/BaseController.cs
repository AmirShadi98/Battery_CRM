using Microsoft.AspNetCore.Mvc;

namespace Battery_CRM.Endpoints.Api.Controllers;

[Route("api/v{version:apiversion}/[controller]")]
public class BaseController : ControllerBase
{
    #region Protected Members
    private string GetInnerException(Exception ex)
    {
        if (ex.InnerException != null)
        {
            return
                $"{ex.InnerException.Message + "( \n " + ex.Message + " \n )"} > {GetInnerException(ex.InnerException)} ";
        }
        return string.Empty;
    }
    #endregion
}
