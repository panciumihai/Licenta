using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CampusManagement.Business.Student;
using CampusManagement.Business.Student.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace CampusManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        //[Authorize(Roles="Student")]
        [HttpGet(Name = "GetAllStudents")]
        [EnableQuery()]
        public async Task<IActionResult> Get()
        {
            var students = await _studentService.GetAllAsync("Person");

            return Ok(students);
        }

        [HttpGet("{id}",Name = "GetStudentById")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _studentService.GetAsync(id,"Person");

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StudentCreateModel studentCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _studentService.AddAsync(studentCreateModel);

            return CreatedAtRoute("GetStudentById", new {id = result}, result);
        }


        [HttpPost("bulk")]
        public async Task<IActionResult> Post([FromBody] IEnumerable<StudentCreateModel> studentCreateModels)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var results = await _studentService.AddAsync(studentCreateModels);

            return CreatedAtRoute("GetAllStudents", results);
        }


        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] StudentCreateModel studentCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _studentService.UpdateAsync(id, studentCreateModel);

            if (result == Guid.Empty)
                return NotFound();

            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _studentService.DeleteAsync(id);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] IEnumerable<Guid> ids)
        {
            await _studentService.DeleteAsync(ids);
            return NoContent();
        }
    }
}