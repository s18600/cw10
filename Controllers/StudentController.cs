using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/Student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        IDbService _service;

        public StudentController(IDbService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("test");
        }

        [HttpGet("/all")]
        public IActionResult GetAllStudents()
        {
            return Ok(this._service.GetAllStudtens());
        }

        [HttpPost]
        public IActionResult ChangeStudent(Student student)
        {

            string str = _service.UpdateStudent(student);
            if (str != "Ok")
                return BadRequest("Index number doesnt exist");

            return Ok(str);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(String id)
        {

            string str = _service.DeleteStudent(id);
            if (str != "Ok")
                return BadRequest(str);

            return Ok(str);

        }


    }
}
