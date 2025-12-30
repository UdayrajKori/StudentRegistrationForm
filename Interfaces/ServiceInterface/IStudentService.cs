using StudentRegistrationForm.DTOs.RequestDTOs;
using StudentRegistrationForm.Models;

namespace StudentRegistrationForm.Interfaces.ServiceInterface
{
    public interface IStudentService
    {
        //Task<Student> GetStudentByIdAsync(int id);
        Task<Student> GetStudentByIdAsync(int id);
        Task<List<Student>> GetAllStudentsAsync();
        Task AddStudentAsync(CompleteRequestDTO dto);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(Student student);
    }
}
