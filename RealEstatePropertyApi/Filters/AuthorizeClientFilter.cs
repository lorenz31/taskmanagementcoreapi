using CoreApiProject.Core.Helpers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;

namespace CoreApiProject.Filters
{
    public class AuthorizeClientFilter : Attribute, IAuthorizationFilter
    {
        public AuthorizeClientFilter() { }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            StringValues clientid = string.Empty;

            var isHeaderPresent = context.HttpContext.Request.Headers.TryGetValue("x-clientid", out clientid);

            if (isHeaderPresent)
            {
                if (clientid != ClientAppHelper.ClientApp)
                {
                    context.Result = new UnauthorizedResult();
                }
            }

            if (!isHeaderPresent)
                context.Result = new UnauthorizedResult();
        }
    }
}
