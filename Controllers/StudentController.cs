using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentRegistrationForm.DTOs.RequestDTOs;
using StudentRegistrationForm.Interfaces.ServiceInterface;
using StudentRegistrationForm.Models;
using System.Threading.Tasks;

namespace StudentRegistrationForm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
       private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterStudent([FromBody] CompleteRequestDTO studentDto)
        {
            if (studentDto == null)
            {
                return BadRequest("Student data is null.");
            }

            try
            {
                await _studentService.AddStudentAsync(studentDto);
                return Ok(new { Message = "Student registered successfully.", success = true});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {message = $"Error: {ex.Message}", success = false});
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                var students = await _studentService.GetAllStudentsAsync();
                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error: {ex.Message}" });
            }
        }
    }
}
