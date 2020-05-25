//using CoreApiProject.Filters;
using CoreApiProject.Core.Services;
using CoreApiProject.Core.BusinessModels;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace RealEstatePropertyApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/v1/Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService _accountService;
        private IResponseModel _response;

        public AccountController(
            IAccountService accountService,
            IResponseModel response)
        {
            _accountService = accountService;
            _response = response;
        }

        [AllowAnonymous]
        //[AuthorizeClientFilter]
        [HttpPost]
        [Route("user/register")]
        public async Task<ActionResult<IResponseModel>> Create([FromBody] UserModel obj)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var isRegistered = await _accountService.RegisterUserAsync(obj);

            _response.Status = isRegistered.Status;
            _response.Message = isRegistered.Message;

            if (_response.Status)
                return Ok(_response);
            else
                return BadRequest(_response);
        }

        //[AllowAnonymous]
        //[AuthorizeClientFilter]
        //[HttpPost]
        //[Route("client/register")]
        //public async Task<ActionResult<IResponseModel>> PostCreateClient([FromBody] ClientModel obj)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState);

        //    var isRegistered = await _accountService.RegisterClientAsync(obj);

        //    _response.Status = isRegistered.Status;
        //    _response.Message = isRegistered.Message;

        //    if (_response.Status)
        //        return Ok(_response);
        //    else
        //        return BadRequest(_response);
        //}
    }
}