using Microsoft.AspNetCore.Mvc;

namespace Complejo.API.Controllers.Base
{
    public abstract class ApiControllerBase : ControllerBase
    {
        protected ApiControllerBase() { }

        protected virtual IActionResult OkIfNotNullNotFoundOtherwise<T>(T content)
        {
            if (content != null)
                return this.Ok(content);
            else
                return this.NotFound();
        }
    }
}
