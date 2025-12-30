using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationForm.Models
{
    public class CitizenshipDetail: BaseEntity
    {
        [Required]
        public Guid StudentPid { get; set; }

        [Required]
        public string CitizenshipNumber { get; set; }

        [Required]
        public DateTime IssueDate { get; set; }

        [Required]
        public string IssueDistrict { get; set; }
    }
}
