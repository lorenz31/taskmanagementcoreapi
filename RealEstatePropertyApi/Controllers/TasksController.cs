using CoreApiProject.Filters;
using CoreApiProject.Core.Services;
using CoreApiProject.Core.BusinessModels;

using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace CoreApiProject.Controllers
{
    //[AuthorizeClientFilter]
    [Authorize]
    [EnableCors("AllowSpecificOrigin")]
    [Produces("application/json")]
    [Route("api/v1/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private ITasksService _tasksService;
        private ICommentService _commentService;
        private IResponseModel _response;

        public TasksController(
            ITasksService tasksService,
            ICommentService commentService,
            IResponseModel response)
        {
            _tasksService = tasksService;
            _commentService = commentService;
            _response = response;
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<IResponseModel>> PostTaskAsync([FromBody] TasksModel obj)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var isTaskAdded = await _tasksService.AddTaskAsync(obj);

            _response.Status = isTaskAdded.Status;
            _response.Message = isTaskAdded.Message;

            if (_response.Status)
                return Ok(_response);
            else
                return BadRequest(_response);
        }

        [HttpGet]
        [Route("project/{projid:guid}")]
        [ServiceFilter(typeof(ValidateProjIdFilter))]
        public async Task<ActionResult<List<ProjectModel>>> GetProjectTasksAsync(Guid projid)
        {
            var ids = HttpContext.Items;

            if (ids.ContainsKey("pid"))
            {
                Guid projId = (Guid)HttpContext.Items["pid"];

                var tasks = await _tasksService.GetTasksPerProjectAsync(projId);

                if (tasks == null) return NoContent();

                return Ok(tasks);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("project/{projid:guid}/task/{taskid:guid}")]
        [ServiceFilter(typeof(ValidateProjTaskIdFilter))]
        public async Task<ActionResult<TasksModel>> GetTasksDetailAsync(Guid projid, Guid taskid)
        {
            var ids = HttpContext.Items;

            if (ids.ContainsKey("pid") && ids.ContainsKey("tid"))
            {
                Guid projId = (Guid)HttpContext.Items["pid"];
                Guid taskId = (Guid)HttpContext.Items["tid"];

                var tasks = await _tasksService.GetTaskDetailAsync(projId, taskId);

                if (tasks == null) return NoContent();

                return Ok(tasks);
            }

            return NotFound();
        }

        [HttpPut]
        [Route("project/{projid:guid}/task/{taskid:guid}/updsts")]
        [ServiceFilter(typeof(ValidateProjTaskIdFilter))]
        public async Task<ActionResult<bool>> PutTaskStatusUpdateAsync(Guid projid, Guid taskid)
        {
            var ids = HttpContext.Items;

            if (ids.ContainsKey("pid") && ids.ContainsKey("tid"))
            {
                Guid projId = (Guid)HttpContext.Items["pid"];
                Guid taskId = (Guid)HttpContext.Items["tid"];

                var isStsUpd = await _tasksService.UpdateTaskStatusAsync(projId, taskId);

                _response.Status = isStsUpd ? true : false;
                _response.Message = isStsUpd ? "Task status updated" : "Error updating status";

                if (_response.Status)
                    return Ok(_response);
                else
                    return BadRequest(_response);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("comment/add")]
        public async Task<ActionResult<IResponseModel>> PostAddCommentAsync([FromBody] CommentModel obj)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var isTaskAdded = await _commentService.AddCommentAsync(obj);

            _response.Status = isTaskAdded.Status;
            _response.Message = isTaskAdded.Message;

            if (_response.Status)
                return Ok(_response);
            else
                return BadRequest(_response);
        }

        [HttpGet]
        [Route("{taskid:guid}/comments")]
        [ServiceFilter(typeof(ValidateTaskIdFilter))]
        public async Task<ActionResult<List<ViewCommentModel>>> GetTaskCommentsAsync(Guid taskid)
        {
            var ids = HttpContext.Items;

            if (ids.ContainsKey("taskid"))
            {
                Guid taskId = (Guid)HttpContext.Items["taskid"];

                var comments = await _commentService.GetCommentsPerTaskAsync(taskId);

                if (comments == null) return NoContent();

                return Ok(comments);
            }

            return NotFound();
        }
    }
}