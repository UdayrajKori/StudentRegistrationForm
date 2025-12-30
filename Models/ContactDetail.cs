using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationForm.Models
{
    public class ContactDetail: BaseEntity
    {
        [Required]
        public Guid StudentPid { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [EmailAddress]
        public string? AlternateEmail { get; set; }

        [Required]
        public string PrimaryMobile { get; set; }

        public string? SecondaryMobile { get; set; }
    }
}
