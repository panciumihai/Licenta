using System.Threading.Tasks;
using CampusManagement.Business.Authentication;
using CampusManagement.Business.Authentication.Models;
using Microsoft.AspNetCore.Mvc;

namespace CampusManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _authenticationService.CreateAccessTokenAsync(loginModel.Email, loginModel.Password);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            //var accessTokenResource = _mapper.Map<AccessToken, AccessTokenResource>(response.Token);
            var accessTokenResource = response.Token;
            return Ok(accessTokenResource);
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenModel refreshTokenModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _authenticationService.RefreshTokenAsync(refreshTokenModel.Token, refreshTokenModel.Email);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            //var tokenResource = _mapper.Map<AccessToken, AccessTokenResource>(response.Token);
            return Ok(response);
        }

        [HttpPost("Revoke")]
        public IActionResult RevokeToken([FromBody] RevokeTokenModel revokeTokenModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _authenticationService.RevokeRefreshToken(revokeTokenModel.Token);
            return NoContent();
        }
    }
}