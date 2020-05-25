using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreApiProject.Controllers
{
    public abstract class ParentController : ControllerBase
    {
        public string BaseUrl() => string.Format("{0}://{1}{2}", HttpContext.Request.Scheme, HttpContext.Request.Host, "/");
    }
}