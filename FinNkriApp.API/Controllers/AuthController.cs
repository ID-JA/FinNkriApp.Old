using FinNkriApp.API.Dtos.Authentication;
using FinNkriApp.API.Identity;
using FinNkriApp.API.Interfaces;
using FinNkriApp.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinNkriApp.API.Controllers
{
    public class AuthController : ApiControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly ITokenClaimsService _tokenClaimsService;
        public AuthController(IIdentityService identityService, ITokenClaimsService tokenClaimsService)
        {
            _identityService = identityService;
            _tokenClaimsService = tokenClaimsService;
        }

        [HttpPost("register")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(AuthenticationResult))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(Result))]
        public async Task<ActionResult> RegisterAsync([FromBody] RegisterUserDto request)
        {
            var result = await _identityService.CreateUserAsync(request, request.Password);

            if (result.Result.Succeeded)
            {
                return Ok(result.AuthenticationResult);
            }
            else
            {
                return BadRequest(result.Result);
            }
        }

    }


}
