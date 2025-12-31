using Microsoft.AspNetCore.Mvc;
using StudentRegistrationForm.DTOs.RequestDTOs;
using StudentRegistrationForm.DTOs.ResponseDTOs;
using StudentRegistrationForm.Interfaces.ServiceInterface;

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
                return BadRequest(new { Message = "Student data is null.", Success = false });

            try
            {
                var response = await _studentService.AddStudentAsync(studentDto);
                return Ok(new 
                { 
                    Message = "Student registered successfully.", 
                    Success = true,
                    Data = response  // Contains Pid, not Id
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Error: {ex.Message}", Success = false });
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                var students = await _studentService.GetAllStudentsAsync();
                return Ok(new 
                { 
                    Message = "Students retrieved successfully.", 
                    Success = true,
                    Count = students.Count,
                    Data = students  // All contain Pid, not Id
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Error: {ex.Message}", Success = false });
            }
        }

        // Public API uses Pid (GUID)
        [HttpGet("{pid:guid}")]
        public async Task<IActionResult> GetStudentByPid(Guid pid)
        {
            try
            {
                var student = await _studentService.GetStudentByPidAsync(pid);
                return Ok(new 
                { 
                    Message = "Student retrieved successfully.", 
                    Success = true,
                    Data = student  // Contains Pid, not Id
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message, Success = false });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Error: {ex.Message}", Success = false });
            }
        }

        [HttpPut("{pid:guid}")]
        public async Task<IActionResult> UpdateStudent(Guid pid, [FromBody] CompleteRequestDTO studentDto)
        {
            if (studentDto == null)
                return BadRequest(new { Message = "Student data is null.", Success = false });

            try
            {
                var response = await _studentService.UpdateStudentAsync(pid, studentDto);
                return Ok(new
                {
                    Message = "Student updated successfully.",
                    Success = true,
                    Data = response  // Contains Pid, not Id
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message, Success = false });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Error: {ex.Message}", Success = false });
            }
        }

        [HttpDelete("{pid:guid}")]
        public async Task<IActionResult> DeleteStudent(Guid pid)
        {
            try
            {
                await _studentService.DeleteStudentAsync(pid);
                return Ok(new 
                { 
                    Message = "Student deleted successfully.", 
                    Success = true
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message, Success = false });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Error: {ex.Message}", Success = false });
            }
        }
    }
}
