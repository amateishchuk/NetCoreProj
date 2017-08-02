using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCore.Dal;
using AspNetCore.Models;
using AspNetCore.Services;
using Microsoft.Extensions.Logging;
using AspNetCore.CustomLogger;
using System.IO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetCore.Controllers
{
    [Route("api/[controller]")]
    public class StudentsController : Controller
    {
        private IStudentRepository studentRepository;

        public StudentsController(IStudentRepository studentRepository, 
            DateTimeService dateTimeService)
        {
            this.studentRepository = studentRepository;
            if (this.studentRepository.GetAllStudents().Count() == 0)
                studentRepository.SaveStudent(
                    new Student { FirstName = "Andrii", LastName = "Mateishchuk" }
                );
            dateTimeService.GetTime();
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(studentRepository.GetAllStudents());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(studentRepository.GetStudent(id));
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Student student)
        {
            if (ModelState.IsValid)
            {
                studentRepository.SaveStudent(student);
                return Ok();
            }
            return BadRequest();            
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Student student)
        {
            if (ModelState.IsValid && id == student.Id)
            {
                studentRepository.UpdateStudent(student);
                return Ok();
            }
            return BadRequest();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var student = studentRepository.GetStudent(id);
            if (student != null)
            {
                studentRepository.UpdateStudent(student);
                return Ok();
            }
            return BadRequest();
        }
    }
}
