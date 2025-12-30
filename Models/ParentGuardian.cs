using StudentRegistrationForm.EnumValues;
using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationForm.Models
{
    public class ParentGuardian : BaseEntity
    {
        [Required]
        public Guid StudentPid { get; set; }

        [Required]
        public GardianType ParentType { get; set; }

        [Required]
        public string FullName { get; set; }
        public string? Occupation { get; set; }
        public string? Designation { get; set; }
        public string? Organization { get; set; }

        [Required]
        public string MobileNumber { get; set; }

        public string? GardianEmail { get; set; }
        public AnnualIncome? AnnualFamilyIncome { get; set; }
    }
}
