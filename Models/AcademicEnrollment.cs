using StudentRegistrationForm.EnumValues;
using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationForm.Models
{
    public class AcademicEnrollment : BaseEntity
    {
        [Required]
        public Guid StudentPid { get; set; }  

        [Required]
        public Faculty Faculty { get; set; }    

        [Required]
        public AcademicProgram Program { get; set; }   

        [Required]
        public Level Level { get; set; }      

        [Required]
        public AcademicYear AcademicYear { get; set; } 

        [Required]
        public Semester Semester { get; set; } 

        [Required]
        public Section Section { get; set; }

        [Required]
        public string RollNumber { get; set; }

        [Required]
        public string RegistrationNumber { get; set; }

        [Required]
        public DateTime EnrollmentDate { get; set; }

        [Required]
        public AcademicStatus AcademicStatus { get; set; }
    }
}
