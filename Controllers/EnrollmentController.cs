using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        IDbService _service;
        public EnrollmentController(IDbService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult EnrollStudent(StudentEnrollmentRequest request)
        {
            var str = _service.EnrollStudent(request);
            if (str != "Ok") return BadRequest(str);
            return Ok(str);
        }

        [HttpPost("promotion")]
        public IActionResult Promotion([FromBody]StudentsPromotionRequest req)
        {
            var str = _service.PromoteStudents(req);
            return Ok(str);

        }

    }
}
