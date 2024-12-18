using Microsoft.AspNetCore.Mvc;

using System.Net;

namespace PiPlateRelayServer.Controllers
{
    public class APIBaseController : ControllerBase
    {
        public JsonResult JsonFailure(object? value)
        {
            return new JsonResult(value)
            {
                StatusCode = (int)HttpStatusCode.NotFound
            };
        }

        public JsonResult JsonSuccess(object? value)
        {
            return new JsonResult(value);
        }
    }
}
