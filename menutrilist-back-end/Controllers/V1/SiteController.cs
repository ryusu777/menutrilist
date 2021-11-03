using Menutrilist.Contracts.V1.Requests;
using Menutrilist.Repository.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Menutrilist.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SiteController : ControllerBase
    {
        private readonly IIdentityRepo _identityService;

        public SiteController(IIdentityRepo identityService)
        {
            _identityService = identityService;
        }

        // GET: api/v1/site
        // [HttpGet]
        // public async Task<IActionResult> GetIdentity()
        // {
        //     int userId = HttpContext.GetUserId();
        //     AuthUserResponse userResponse = await _identityService.GetUserResponse(userId);
        //     if (userResponse == null)
        //     {
        //         return NotFound();
        //     }

        //     return Ok(userResponse);
        // }

        [AllowAnonymous]
        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] AuthSignupRequest request)
        {
            // var origin = _uriService.GetRouteString("/");
            var signupResult = await _identityService.SignUpAsync(request, "http://localhost:5000/");

            return Ok(signupResult);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> SignIn([FromBody] AuthLoginRequest request)
        {
            var authResponse = await _identityService.AuthenticateAsync(request.UserName, request.Password);

            if (!authResponse.Succeeded)
            {
                return BadRequest(authResponse);
            }

            return Ok(authResponse);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] AuthRefreshTokenRequest request)
        {
            var authResponse = await _identityService.RefreshTokenAsync(request.Token, request.RefreshToken);

            if (!authResponse.Succeeded)
            {
                return Unauthorized(authResponse);
            }

            return Ok(authResponse);
        }

        [AllowAnonymous]
        [HttpPost("link-token")]
        public async Task<IActionResult> GetLinkToken([FromBody] AuthTokenRequest request)
        {
            var authResponse = await _identityService.GetLinkTokenAsync(request.LinkToken);

            if (!authResponse.Succeeded)
            {
                return BadRequest(authResponse);
            }

            return Ok(authResponse);
        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] AuthEmailRequest request)
        {
            // var origin = _uriService.GetRouteString("/");
            var authResponse = await _identityService.ForgotPasswordAsync(request.Email, "http://localhost:5000/");

            if (!authResponse.Succeeded)
            {
                return BadRequest(authResponse);
            }

            return Ok(authResponse);
        }

        [AllowAnonymous]
        [HttpPost("resend-verify-email")]
        public async Task<IActionResult> ResendVerifyEmail([FromBody] AuthEmailRequest request)
        {
            // var origin = _uriService.GetRouteString("/");
            var authResponse = await _identityService.ResendConfirmEmailAsync(request.Email, "http://localhost:5000/");

            if (!authResponse.Succeeded)
            {
                return BadRequest(authResponse);
            }

            return Ok(authResponse);
        }

        [AllowAnonymous]
        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromBody] AuthVerifyEmailRequest request)
        {
            // var origin = _uriService.GetRouteString("/");
            var authResponse = await _identityService.VerifyEmailAsync(request, "http://localhost:5000/");

            if (!authResponse.Succeeded)
            {
                return BadRequest(authResponse);
            }

            return Ok(authResponse);
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] AuthResetPasswordRequest request)
        {
            // var origin = _uriService.GetRouteString("/");
            var authResponse = await _identityService.ResetPasswordAsync(request, "http://localhost:5000/");

            if (!authResponse.Succeeded)
            {
                return BadRequest(authResponse);
            }

            return Ok(authResponse);
        }

    }
}
