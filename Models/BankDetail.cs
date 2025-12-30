using StudentRegistrationForm.EnumValues;
using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationForm.Models
{
    public class BankDetail: BaseEntity
    {
        [Required]
        public Guid StudentPid { get; set; }

        [Required]
        public string AccountHolderName { get; set; }

        [Required]
        public BankName BankName { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public string Branch { get; set; }
    }
}
