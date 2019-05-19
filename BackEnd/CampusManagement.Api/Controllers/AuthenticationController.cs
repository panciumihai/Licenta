using System;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public AuthenticationController(IAuthenticationService authenticationService, IMapper mapper)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = "GetRoles")]
        public async Task<IActionResult> GetRolesById(Guid id)
        {
            var result = await _authenticationService.GetRolesByPersonId(id);
            return Ok(result);
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

            var accessTokenResource = _mapper.Map<TokenDetailsModel>(response.Token);
            //var accessTokenResource = response.Token;
            return Created("", accessTokenResource);
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
            var accessTokenResource = _mapper.Map<TokenDetailsModel>(response.Token);
            return Created("", accessTokenResource);
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