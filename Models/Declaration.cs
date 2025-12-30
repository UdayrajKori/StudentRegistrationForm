using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationForm.Models
{
    public class Declaration: BaseEntity
    {
        [Required]
        public Guid StudentPid { get; set; }

        [Required]
        public bool IsAgreed { get; set; }

        [Required]
        public DateTime ApplicationDate { get; set; }

        [Required]
        public string Place { get; set; }
    }
}
