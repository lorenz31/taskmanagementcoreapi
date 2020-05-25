using CoreApiProject.DAL.DataContext;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace CoreApiProject.Filters
{
    public class ValidateProjTaskIdFilter : Attribute, IActionFilter
    {
        private readonly DatabaseContext _db;

        public ValidateProjTaskIdFilter(DatabaseContext db)
        {
            _db = db;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Guid projId = Guid.Empty;
            Guid taskId = Guid.Empty;

            if (context.ActionArguments.ContainsKey("projid") && context.ActionArguments.ContainsKey("taskid"))
            {
                projId = (Guid)context.ActionArguments["projid"];
                taskId = (Guid)context.ActionArguments["taskid"];

                if (!string.IsNullOrEmpty(projId.ToString()) && !string.IsNullOrEmpty(taskId.ToString()))
                {
                    var task = _db.Tasks.Where(t => t.ProjectId == projId && t.Id == taskId).SingleOrDefault();

                    if (task != null)
                    {
                        context.HttpContext.Items.Add("pid", task.ProjectId);
                        context.HttpContext.Items.Add("tid", task.Id);
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
