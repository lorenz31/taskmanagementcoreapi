using CoreApiProject.Core.BusinessModels;
using CoreApiProject.Core.Services;
using CoreApiProject.Filters;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CoreApiProject.Controllers
{
    [Authorize]
    [EnableCors("AllowSpecificOrigin")]
    [Produces("application/json")]
    [Route("api/v1/members")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private IMemberService _memberService;
        private IResponseModel _response;

        public MemberController(
            IMemberService memberService,
            IResponseModel response)
        {
            _memberService = memberService;
            _response = response;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> PostProjectAsync([FromBody] MemberModel obj)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var isMemberAdded = await _memberService.AddNewMemberAsync(obj);

            _response.Status = isMemberAdded.Status;
            _response.Message = isMemberAdded.Message;

            if (_response.Status)
                return Ok(_response);
            else
                return BadRequest(_response);
        }

        [HttpGet]
        [Route("")]
        [ServiceFilter(typeof(ValidateUserIdFilter))]
        public async Task<IActionResult> GetMembersPerUserAsync([FromQuery] Guid userid)
        {
            var ids = HttpContext.Items;

            if (ids.ContainsKey("uid"))
            {
                Guid userId = (Guid)HttpContext.Items["uid"];

                var members = await _memberService.GetMembersPerUserAsync(userId);

                if (members == null) return NoContent();

                return Ok(members);
            }

            return NoContent();
        }
    }
}