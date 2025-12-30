using StudentRegistrationForm.EnumValues;
using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationForm.Models
{
    public class Address : BaseEntity
    {
        [Required]
        public Guid StudentPid { get; set; }

        [Required]
        public AddressType AddressType { get; set; }

        [Required]
        public string Province { get; set; }

        [Required]
        public string District { get; set; }

        [Required]
        public string Municipality { get; set; }

        [Required]
        public int WardNumber { get; set; }

        [Required]
        public string ToleStreet { get; set; }

        public string? HouseNumber { get; set; }
    }
}
