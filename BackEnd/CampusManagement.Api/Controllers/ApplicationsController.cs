﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CampusManagement.Business.Application;
using CampusManagement.Business.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CampusManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationsController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet(Name="GetAllApplication")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _applicationService.GetAllAsync("HostelPreferences");
            return Ok(result);
        }


        [HttpGet("{id}", Name = "GetApplicationById")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _applicationService.GetAsync(id, "HostelPreferences");
            return Ok(result);
        }

        [Authorize(Roles = "Student, Admin")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ApplicationCreateModel applicationCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _applicationService.AddAsync(applicationCreateModel);

            return CreatedAtRoute("GetApplicationById", new { id = result }, result);
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> Post([FromBody] IEnumerable<ApplicationCreateModel> applicationCreateModels)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var results = await _applicationService.AddAsync(applicationCreateModels);

            return CreatedAtRoute("GetAllApplication", results);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] ApplicationCreateModel applicationCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _applicationService.UpdateAsync(id, applicationCreateModel);

            if (result == Guid.Empty)
                return NotFound();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _applicationService.DeleteAsync(id);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] IEnumerable<Guid> ids)
        {
            await _applicationService.DeleteAsync(ids);
            return NoContent();
        }
    }
}