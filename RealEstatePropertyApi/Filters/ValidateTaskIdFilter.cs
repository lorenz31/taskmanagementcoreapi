using CoreApiProject.DAL.DataContext;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace CoreApiProject.Filters
{
    public class ValidateTaskIdFilter : Attribute, IActionFilter
    {
        private readonly DatabaseContext _db;

        public ValidateTaskIdFilter(DatabaseContext db)
        {
            _db = db;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Guid taskId = Guid.Empty;

            if (context.ActionArguments.ContainsKey("taskid"))
            {
                taskId = (Guid)context.ActionArguments["taskid"];

                if (!string.IsNullOrEmpty(taskId.ToString()))
                {
                    var taskInfo = _db.Tasks.Where(t => t.Id == taskId).SingleOrDefault();

                    if (taskInfo != null)
                    {
                        context.HttpContext.Items.Add("taskid", taskInfo.Id);
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
