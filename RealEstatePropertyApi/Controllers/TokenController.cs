//using CoreApiProject.Filters;
using CoreApiProject.Core.Services;
using CoreApiProject.Core.BusinessModels;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace RealEstatePropertyApi.Controllers
{
    [Authorize]
    [EnableCors("AllowSpecificOrigin")]
    [Produces("application/json")]
    [Route("api/v1/Token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IAccountService _accountService;

        public TokenController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        //[AuthorizeClientFilter]
        [HttpPost]
        [Route("user")]
        public async Task<ActionResult<TokenModel>> Create([FromBody] UserModel obj)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userVerified = await _accountService.VerifyUserAsync(obj);

            if (userVerified == null) return BadRequest(new ResponseModel { Status = false, Message = "Invalid user." });

            var token = _accountService.GenerateJwt(userVerified);

            return Ok(token);
        }

        //[AllowAnonymous]
        //[AuthorizeClientFilter]
        //[HttpPost]
        //[Route("client")]
        //public async Task<ActionResult<TokenModel>> PostGenerateToken([FromBody] ClientModel obj)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState);

        //    var clientVerified = await _accountService.VerifyClientAsync(obj);

        //    if (clientVerified == null) return BadRequest(new ResponseModel { Status = false, Message = "Invalid client." });

        //    var token = _accountService.GenerateJwt(clientVerified);

        //    return Ok(token);
        //}
    }
}