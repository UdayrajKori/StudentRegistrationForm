using StudentRegistrationForm.EnumValues;
using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationForm.Models
{
    public class StudentDocument: BaseEntity
    {
        [Required]
        public Guid StudentPid { get; set; }

        [Required]
        public DocumentType DocumentType { get; set; }

        [Required]
        public string FilePath { get; set; }
    }
}
