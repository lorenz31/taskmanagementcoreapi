using CoreApiProject.DAL.DataContext;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace CoreApiProject.Filters
{
    public class ValidateUserIdFilter : Attribute, IActionFilter
    {
        private readonly DatabaseContext _db;

        public ValidateUserIdFilter(DatabaseContext db)
        {
            _db = db;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Guid userId = Guid.Empty;

            if (context.ActionArguments.ContainsKey("userid"))
            {
                userId = (Guid)context.ActionArguments["userid"];

                if (!string.IsNullOrEmpty(userId.ToString()))
                {
                    var userInfo = _db.Users.Where(r => r.Id == userId).SingleOrDefault();

                    if (userInfo != null)
                    {
                        context.HttpContext.Items.Add("uid", userInfo.Id);
                    }
                }
            }
            else
            {
                context.Result = new BadRequestResult();
                return;
            }
        }
    }
}
