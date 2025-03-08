using Microsoft.AspNetCore.Mvc;
using StudyDS_web.Data;

namespace StudyDS_web.Controllers
{
    public class ControllerBase : Controller
    {
        protected void HandleWebApiException(WebApiExceptions ex)
        {
            if (ex.ErrorResponse != null && ex.ErrorResponse.Errors != null && ex.ErrorResponse.Errors.Count > 0)
            {
                foreach (var error in ex.ErrorResponse.Errors)
                {
                    ModelState.AddModelError(error.Key, string.Join(", ", error.Value));
                }
            }
        }
    }
}
