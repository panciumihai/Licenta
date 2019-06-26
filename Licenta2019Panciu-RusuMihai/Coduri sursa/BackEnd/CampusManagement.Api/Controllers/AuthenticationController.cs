﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using CampusManagement.Business.Authentication;
using CampusManagement.Business.Authentication.Models;
using Microsoft.AspNetCore.Authorization;
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

            var response = await _authenticationService.Authenticate(loginModel.Email, loginModel.Password);
            if (response == null)
            {
                return BadRequest("Incorrect Email or Parssword!");
            }

            return Created("", response);
        }
    }
}