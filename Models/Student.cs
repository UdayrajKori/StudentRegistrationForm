using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationForm.Models
{
    public class Student: BaseEntity
    {
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]  
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string PlaceOfBirth { get; set; }
        [Required]
        public string PhotoPath { get; set; }
    }

    // navigation properties
}
