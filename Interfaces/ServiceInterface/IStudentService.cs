using StudentRegistrationForm.DTOs.RequestDTOs;
using StudentRegistrationForm.DTOs.ResponseDTOs;
using StudentRegistrationForm.Models;

namespace StudentRegistrationForm.Interfaces.ServiceInterface
{
    public interface IStudentService
    {
        // Internal methods use Id (int) - NOT exposed to API
        Task<CompleteResponseDTO> GetStudentByIdAsync(int id);
        
        // Public API methods use Pid (Guid)
        Task<CompleteResponseDTO> GetStudentByPidAsync(Guid pid);
        
        Task<List<CompleteResponseDTO>> GetAllStudentsAsync();
        Task<CompleteResponseDTO> AddStudentAsync(CompleteRequestDTO dto);
        Task<CompleteResponseDTO> UpdateStudentAsync(Guid pid, CompleteRequestDTO dto);
        Task DeleteStudentAsync(Guid pid);  // Use Pid for public API
    }
}
