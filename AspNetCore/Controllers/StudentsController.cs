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
        private IStudentRepositoryService studentRepository;
        private ILogger logger;
        private DateTimeService dateTimeService;

        public StudentsController(IStudentRepositoryService studentRepository, 
            DateTimeService dateTimeService,
            ILogger<StudentsController> logger)
        {
            this.studentRepository = studentRepository;
            if (this.studentRepository.GetAllStudents().Count() == 0)
                studentRepository.SaveStudent(
                    new Student { FirstName = "Andrii", LastName = "Mateishchuk" }
                );
            this.logger = logger;
            this.dateTimeService = dateTimeService;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            logger.LogInformation($"{dateTimeService.GetTime()}: Getting Data");
            return Ok(studentRepository.GetAllStudents());            
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var student = studentRepository.GetStudent(id);
            if (student == null)
            {
                logger.LogWarning($"{dateTimeService.GetTime()}: Student not found");
                return NotFound();
            }

            logger.LogInformation($"{dateTimeService.GetTime()}: Getting student");
            return Ok(student);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Student student)
        {
            if (ModelState.IsValid)
            {
                studentRepository.SaveStudent(student);
                logger.LogInformation($"{dateTimeService.GetTime()}: Inserting student");
                return Ok();
            }
            logger.LogWarning($"{dateTimeService.GetTime()}: Invalid student model");
            return BadRequest();            
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Student student)
        {
            if (ModelState.IsValid && id == student.Id)
            {
                studentRepository.UpdateStudent(student);
                logger.LogInformation($"{dateTimeService.GetTime()}: Updating student");
                return Ok();
            }
            logger.LogWarning($"{dateTimeService.GetTime()}: Invalid input data");
            return BadRequest();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var student = studentRepository.GetStudent(id);
            if (student != null)
            {
                logger.LogInformation($"{dateTimeService.GetTime()}: Deleting student");
                studentRepository.UpdateStudent(student);
                return Ok();
            }
            logger.LogWarning($"{dateTimeService.GetTime()}: Student not found");
            return BadRequest();
        }
    }
}
