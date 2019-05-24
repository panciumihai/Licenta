using System;
using System.Threading.Tasks;
using CampusManagement.Business.HostelStatus;
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
            var result = await _hostelStatusService.GetAllAsync("Hostel","StudentsGroups.Students");
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            await _hostelStatusService.GetAsync(id, "Hostel", "StudentsGroups.Students");
            return Ok();
        }

        // POST: api/HostelStatus
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/HostelStatus/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
