using StudentRegistrationForm.EnumValues;
using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationForm.Models
{
    public class ExtracurricularDetail: BaseEntity
    {
        [Required]
        public Guid StudentPid { get; set; }   // FK → Student.Pid

        public string? Interests { get; set; }  // Comma-separated list: Sports, Music, Coding, Other

        public string? Achievements { get; set; } // Comma-separated: Title | Organization | Year

        [Required]
        public ScholarType ScholarType { get; set; }  // Hosteller / Day Scholar

        public TransportationMethod? TransportMethod { get; set; }
    }
}
