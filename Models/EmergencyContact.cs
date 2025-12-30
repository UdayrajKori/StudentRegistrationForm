using StudentRegistrationForm.EnumValues;
using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationForm.Models
{
    public class EmergencyContact: BaseEntity
    {
        [Required]
        public Guid StudentPid { get; set; }

        [Required]
        public string ContactName { get; set; }

        [Required]
        public GardianType Relation { get; set; }

        [Required]
        public string ContactNumber { get; set; }
    }
}
