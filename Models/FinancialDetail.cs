using StudentRegistrationForm.EnumValues;
using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationForm.Models
{
    public class FinancialDetail: BaseEntity
    {
        [Required]
        public Guid StudentPid { get; set; }

        [Required]
        public FeeCategory FeeCategory { get; set; }

        public ScholarshipType ScholarshipType { get; set; }
        public string? ScholarshipProviderName { get; set; }
        public decimal? ScholarshipAmount { get; set; }
    }
}
