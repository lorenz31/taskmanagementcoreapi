using CoreApiProject.DAL.DataContext;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace CoreApiProject.Filters
{
    public class ValidateProjIdFilter : Attribute, IActionFilter
    {
        private readonly DatabaseContext _db;

        public ValidateProjIdFilter(DatabaseContext db)
        {
            _db = db;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Guid projId = Guid.Empty;

            if (context.ActionArguments.ContainsKey("projid"))
            {
                projId = (Guid)context.ActionArguments["projid"];

                if (!string.IsNullOrEmpty(projId.ToString()))
                {
                    var projInfo = _db.Projects.Where(p => p.Id == projId).SingleOrDefault();

                    if (projInfo != null)
                    {
                        context.HttpContext.Items.Add("pid", projInfo.Id);
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
