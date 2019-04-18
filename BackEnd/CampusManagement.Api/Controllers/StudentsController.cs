using System;
using System.Net;
using System.Threading.Tasks;
using CampusManagement.Business.Student;
using CampusManagement.Business.Student.Models;
using Microsoft.AspNetCore.Mvc;

namespace CampusManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService studentService;

        public StudentsController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var students = await studentService.GetAllAsync();
            return Ok(students);
        }

        [HttpGet("{id}",Name = "GetById")]
        public async Task<IActionResult> Get(Guid id)
        {
            var student = await studentService.GetAsync(id);
            return Ok(student);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StudentCreateModel studentCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await studentService.AddAsync(studentCreateModel);
            //return Ok(result);

            return CreatedAtAction("GetById", new { id = result }, result);
            //return result;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}