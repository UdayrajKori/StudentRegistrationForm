using StudentRegistrationForm.EnumValues;
using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationForm.Models
{
    public class DisabilityDetail: BaseEntity
    {
        [Required]
        public Guid StudentPid { get; set; }

        [Required]
        public DisabilityType DisabilityType { get; set; } = DisabilityType.None;

        public int? DisabilityPercentage { get; set; }
    }
}
