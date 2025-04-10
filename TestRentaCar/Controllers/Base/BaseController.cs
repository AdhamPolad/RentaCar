using Microsoft.AspNetCore.Mvc;
using TestRentaCarSln.Buisness.Dtos.Common;

namespace TestRentaCar.Api.Controllers.Base
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        public IActionResult Execute<T>(GenericResponceModel<T> model)
        {
            return StatusCode(model.StatusCode, model);
        }
    }
}
