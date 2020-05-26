using CoreApiProject.Filters;
using CoreApiProject.Core.Services;
using CoreApiProject.Core.BusinessModels;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using System.Threading.Tasks;
using System;

namespace CoreApiProject.Controllers
{
    //[AuthorizeClientFilter]
    [Authorize]
    [EnableCors("AllowSpecificOrigin")]
    [Produces("application/json")]
    [Route("api/v1/projects")]
    [ApiController]
    public class ProjectController : ParentController
    {
        private IProjectService _projectService;
        private IResponseModel _response;

        public ProjectController(
            IProjectService projectService,
            IResponseModel response)
        {
            _projectService = projectService;
            _response = response;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> PostProjectAsync([FromBody] ProjectModel obj)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var isProjectAdded = await _projectService.AddNewProjectAsync(obj);

            _response.Status = isProjectAdded.Status;
            _response.Message = isProjectAdded.Message;

            if (_response.Status)
                return Ok(_response);
            else
                return BadRequest(_response);
        }

        [HttpGet]
        [Route("")]
        [ServiceFilter(typeof(ValidateUserIdFilter))]
        public async Task<IActionResult> GetUserProjectsAsync([FromQuery] Guid userid)
        {
            var ids = HttpContext.Items;

            if (ids.ContainsKey("uid"))
            {
                Guid userId = (Guid)HttpContext.Items["uid"];

                var projects = await _projectService.GetProjectsAsync(userId);

                if (projects == null) return NoContent();

                return Ok(projects);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("active")]
        [ServiceFilter(typeof(ValidateUserIdFilter))]
        public async Task<IActionResult> GetUserActiveProjectsAsync([FromQuery] Guid userid)
        {
            var ids = HttpContext.Items;

            if (ids.ContainsKey("uid"))
            {
                Guid userId = (Guid)HttpContext.Items["uid"];

                var projects = await _projectService.GetActiveProjectsAsync(userId);

                if (projects == null) return NoContent();

                return Ok(projects);
            }

            return NotFound();
        }

        [HttpPut]
        [Route("{userid:guid}/project/{projid:guid}/updsts")]
        [ServiceFilter(typeof(ValidateUserProjIdFilter))]
        public async Task<IActionResult> PutProjectStatusUpdateAsync(Guid userid, Guid projid)
        {
            var ids = HttpContext.Items;

            if (ids.ContainsKey("uid") && ids.ContainsKey("pid"))
            {
                Guid userId = (Guid)HttpContext.Items["uid"];
                Guid projId = (Guid)HttpContext.Items["pid"];

                var isStatusUpdated = await _projectService.UpdateProjectStatusAsync(userId, projId);

                _response.Status = isStatusUpdated ? true : false;
                _response.Message = isStatusUpdated ? "Project status updated" : "Error updating status";

                if (_response.Status)
                    return Ok(_response);
                else
                    return BadRequest(_response);
            }

            _response.Status = false;
            _response.Message = "Error";

            return NotFound(_response);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> PutProjectDetailsUpdateAsync([FromBody] ProjectModel obj)
        {
            var isStatusUpdated = await _projectService.UpdateProjectDetailAsync(obj);

            _response.Status = isStatusUpdated.Status;
            _response.Message = isStatusUpdated.Message;

            if (_response.Status)
                return Ok(_response);
            else
                return BadRequest(_response);
        }

        [HttpPost]
        [Route("assign/member")]
        public async Task<IActionResult> PostAssignProjectMemberAsync([FromBody] MemberProjectsModel obj)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var isProjectAdded = await _projectService.AssignMemberAsync(obj);

            _response.Status = isProjectAdded.Status;
            _response.Message = isProjectAdded.Message;

            if (_response.Status)
                return Ok(_response);
            else
                return BadRequest(_response);
        }
    }
}