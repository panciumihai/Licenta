﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CampusManagement.Business.HostelStatus;
using CampusManagement.Business.HostelStatus.Models;
using Microsoft.AspNetCore.Mvc;

namespace CampusManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostelsStatusController : ControllerBase
    {
        private readonly IHostelStatusService _hostelStatusService;

        public HostelsStatusController(IHostelStatusService hostelStatusService)
        {
            _hostelStatusService = hostelStatusService;
        }

        [HttpGet(Name = "GetAllHostelsStatus")]
        public async Task<IActionResult> Get()
        {
            var result = await _hostelStatusService.GetAllAsync("Hostel", "StudentsGroups", "StudentsGroups.Students");
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetHostelStatusById")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _hostelStatusService.GetAsync(id, "Hostel", "StudentsGroups.Students");
            return Ok(result);
        }

        [HttpGet("Seats",Name = "GetAllHostelStatusSeats")]
        public async Task<IActionResult> GetSeats()
        {
            var result = await _hostelStatusService.GetSeats();
            return Ok(result);
        }

        [HttpGet("SeatsAllocationPreview", Name = "SeatsAllocationPreview")]
        public async Task<IActionResult> SeatsAllocation()
        {
            var result = await _hostelStatusService.SeatsAllocationPreview();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] HostelStatusCreateModel hostelStatusCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _hostelStatusService.AddOrUpdate(hostelStatusCreateModel);

            return CreatedAtRoute("GetHostelStatusById", new { id = result }, result);
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> Post([FromBody] IEnumerable<HostelStatusCreateModel> hostelStatusCreateModels)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var results = await _hostelStatusService.AddOrUpdate(hostelStatusCreateModels);

            return CreatedAtRoute("GetAllHostel", results);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _hostelStatusService.DeleteAsync(id);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] IEnumerable<Guid> ids)
        {
            await _hostelStatusService.DeleteAsync(ids);
            return NoContent();
        }
    }
}
