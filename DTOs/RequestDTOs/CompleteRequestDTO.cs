using StudentRegistrationForm.EnumValues;
using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationForm.DTOs.RequestDTOs
{
    public class CompleteRequestDTO
    {
        // Student Details
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } = DateTime.MinValue;
        public string PlaceOfBirth { get; set; } = string.Empty;
        public string PhotoPath { get; set; } = string.Empty;


        // Personal Details
        public Gender Gender { get; set; }

        public Nationality Nationality { get; set; } = EnumValues.Nationality.Nepal;

        public BloodGroup? BloodGroup { get; set; } = EnumValues.BloodGroup.O_Positive;

        public MaritalStatus? MaritalStatus { get; set; } = EnumValues.MaritalStatus.Single;

        public string? Religion { get; set; } = string.Empty;

        public string Ethnicity { get; set; } = string.Empty;

        // Parent/Guardian Details
        public GardianType ParentType { get; set; } = GardianType.Father;
        public string FullName { get; set; } = string.Empty;
        public string? Occupation { get; set; } = string.Empty;
        public string? Designation { get; set; } = string.Empty;
        public string? Organization { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string? GardianEmail { get; set; } = string.Empty;
        public AnnualIncome? AnnualFamilyIncome { get; set; } = AnnualIncome.Between5And10Lakh;

        // Contact Details
        public string Email { get; set; } = string.Empty;
        public string? AlternateEmail { get; set; } = string.Empty;
        public string PrimaryMobile { get; set; } = string.Empty;
        public string? SecondaryMobile { get; set; } = string.Empty;

        // Emergency Contact Details
        public string ContactName { get; set; } = string.Empty;
        public GardianType Relation { get; set; } = GardianType.Father;
        public string ContactNumber { get; set; } = string.Empty;

        // Address Details
        [Required]
        public AddressType AddressType { get; set; } = AddressType.Permanent;
        public string Province { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string Municipality { get; set; } = string.Empty;
        public int WardNumber { get; set; } = 0;
        public string ToleStreet { get; set; } = string.Empty;
        public string? HouseNumber { get; set; } = string.Empty;


        // Disability Details
        public DisabilityType DisabilityType { get; set; } = DisabilityType.None;
        public int? DisabilityPercentage { get; set; } = 0;


        // Financial details
        public FeeCategory FeeCategory { get; set; } = 0;
        public ScholarshipType ScholarshipType { get; set; } = 0;
        public string? ScholarshipProviderName { get; set; } = string.Empty;
        public decimal? ScholarshipAmount { get; set; } = 0;

        // Bank Details
        public string AccountHolderName { get; set; } = string.Empty;
        public BankName BankName { get; set; } = 0;
        public string AccountNumber { get; set; } = string.Empty;
        public string Branch { get; set; } = string.Empty;

        // Citizenship Details
        public string CitizenshipNumber { get; set; } = string.Empty;
        public DateTime IssueDate { get; set; } = DateTime.MinValue;
        public string IssueDistrict { get; set; } = string.Empty;

        // Academic History
        public Qualification Qualification { get; set; } = 0;
        public string BoardOrUniversity { get; set; } = string.Empty;
        public string InstitutionName { get; set; } = string.Empty;
        public int PassedYear { get; set; } = 0;
        public string DivisionOrGPA { get; set; } = string.Empty;
        public string? MarksheetPath { get; set; } = string.Empty;

        // Academic Enrollment Details
        public Faculty Faculty { get; set; } = 0;
        public AcademicProgram Program { get; set; } = 0;
        public Level Level { get; set; } = 0;
        public AcademicYear AcademicYear { get; set; } = 0;
        public Semester Semester { get; set; } = 0;
        public Section Section { get; set; } = 0;
        public string RollNumber { get; set; } = string.Empty;
        public string RegistrationNumber { get; set; } = string.Empty;
        public DateTime EnrollmentDate { get; set; } = DateTime.MinValue;
        public AcademicStatus AcademicStatus { get; set; } = 0;

        // Extra Curricular Activities
        public string? Interests { get; set; } = string.Empty;
        public string? Achievements { get; set; } = string.Empty;
        public ScholarType ScholarType { get; set; } = 0;
        public TransportationMethod? TransportMethod { get; set; } = 0;

        // Student Documents
        public DocumentType DocumentType { get; set; } = 0;
        public string FilePath { get; set; } = string.Empty;

        // Declaration
        public bool IsAgreed { get; set; } = false;
        public DateTime ApplicationDate { get; set; } = DateTime.MinValue;
        public string Place { get; set; } = string.Empty;
    }


}
