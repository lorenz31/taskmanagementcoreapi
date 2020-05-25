using CoreApiProject.DAL.DataContext;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using System;
using System.Linq;

namespace CoreApiProject.Filters
{
    public class ValidateUserProjIdFilter : Attribute, IActionFilter
    {
        private readonly DatabaseContext _db;

        public ValidateUserProjIdFilter(DatabaseContext db)
        {
            _db = db;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Guid userId = Guid.Empty;
            Guid projId = Guid.Empty;

            if (context.ActionArguments.ContainsKey("userid") && context.ActionArguments.ContainsKey("projid"))
            {
                userId = (Guid)context.ActionArguments["userid"];
                projId = (Guid)context.ActionArguments["projid"];

                if (!string.IsNullOrEmpty(userId.ToString()) && !string.IsNullOrEmpty(projId.ToString()))
                {
                    var project = _db.Projects.Where(p => p.UserId == userId && p.Id == projId).SingleOrDefault();

                    if (project != null)
                    {
                        context.HttpContext.Items.Add("uid", project.UserId);
                        context.HttpContext.Items.Add("pid", project.Id);
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
