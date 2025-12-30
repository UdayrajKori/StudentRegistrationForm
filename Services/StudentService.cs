using StudentRegistrationForm.DTOs.RequestDTOs;
using StudentRegistrationForm.Interfaces;
using StudentRegistrationForm.Interfaces.ServiceInterface;
using StudentRegistrationForm.Models;

namespace StudentRegistrationForm.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddStudentAsync(CompleteRequestDTO dto)
        {
            var now = DateTime.UtcNow;

            // Create Student with all related entities using navigation properties
            var student = new Student
            {
                // Student basic info
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                LastName = dto.LastName,
                DateOfBirth = dto.DateOfBirth,
                PlaceOfBirth = dto.PlaceOfBirth,
                PhotoPath = dto.PhotoPath,
                CreatedOn = now,
                UpdatedOn = now,

                // One-to-One relationships via navigation properties
                PersonalDetails = new PersonalDetails
                {
                    Gender = dto.Gender,
                    Nationality = dto.Nationality,
                    BloodGroup = dto.BloodGroup,
                    MaritalStatus = dto.MaritalStatus,
                    Religion = dto.Religion,
                    Ethnicity = dto.Ethnicity,
                    CreatedOn = now,
                    UpdatedOn = now
                },

                ContactDetail = new ContactDetail
                {
                    Email = dto.Email,
                    AlternateEmail = dto.AlternateEmail,
                    PrimaryMobile = dto.PrimaryMobile,
                    SecondaryMobile = dto.SecondaryMobile,
                    CreatedOn = now,
                    UpdatedOn = now
                },

                FinancialDetail = new FinancialDetail
                {
                    FeeCategory = dto.FeeCategory,
                    ScholarshipType = dto.ScholarshipType,
                    ScholarshipProviderName = dto.ScholarshipProviderName,
                    ScholarshipAmount = dto.ScholarshipAmount,
                    CreatedOn = now,
                    UpdatedOn = now
                },

                BankDetail = new BankDetail
                {
                    AccountHolderName = dto.AccountHolderName,
                    BankName = dto.BankName,
                    AccountNumber = dto.AccountNumber,
                    Branch = dto.Branch,
                    CreatedOn = now,
                    UpdatedOn = now
                },

                CitizenshipDetail = new CitizenshipDetail
                {
                    CitizenshipNumber = dto.CitizenshipNumber,
                    IssueDate = dto.IssueDate,
                    IssueDistrict = dto.IssueDistrict,
                    CreatedOn = now,
                    UpdatedOn = now
                },

                AcademicEnrollment = new AcademicEnrollment
                {
                    Faculty = dto.Faculty,
                    Program = dto.Program,
                    Level = dto.Level,
                    AcademicYear = dto.AcademicYear,
                    Semester = dto.Semester,
                    Section = dto.Section,
                    RollNumber = dto.RollNumber,
                    RegistrationNumber = dto.RegistrationNumber,
                    EnrollmentDate = dto.EnrollmentDate,
                    AcademicStatus = dto.AcademicStatus,
                    CreatedOn = now,
                    UpdatedOn = now
                },

                Declaration = new Declaration
                {
                    IsAgreed = dto.IsAgreed,
                    ApplicationDate = dto.ApplicationDate,
                    Place = dto.Place,
                    CreatedOn = now,
                    UpdatedOn = now
                },

                // One-to-Many relationships via navigation properties
                Addresses = dto.Addresses?.Select(a => new Address
                {
                    AddressType = a.AddressType,
                    Province = a.Province,
                    District = a.District,
                    Municipality = a.Municipality,
                    WardNumber = a.WardNumber,
                    ToleStreet = a.ToleStreet,
                    HouseNumber = a.HouseNumber,
                    CreatedOn = now,
                    UpdatedOn = now
                }).ToList() ?? new List<Address>(),

                EmergencyContacts = dto.EmergencyContacts?.Select(c => new EmergencyContact
                {
                    ContactName = c.ContactName,
                    Relation = c.Relation,
                    ContactNumber = c.ContactNumber,
                    CreatedOn = now,
                    UpdatedOn = now
                }).ToList() ?? new List<EmergencyContact>(),

                DisabilityDetails = dto.DisabilityDetails?.Select(d => new DisabilityDetail
                {
                    DisabilityType = d.DisabilityType,
                    DisabilityPercentage = d.DisabilityPercentage,
                    CreatedOn = now,
                    UpdatedOn = now
                }).ToList() ?? new List<DisabilityDetail>(),

                ParentGuardians = dto.ParentGuardians?.Select(p => new ParentGuardian
                {
                    ParentType = p.ParentType,
                    FullName = p.FullName,
                    Occupation = p.Occupation,
                    Designation = p.Designation,
                    Organization = p.Organization,
                    MobileNumber = p.MobileNumber,
                    GardianEmail = p.GardianEmail,
                    AnnualFamilyIncome = p.AnnualFamilyIncome,
                    CreatedOn = now,
                    UpdatedOn = now
                }).ToList() ?? new List<ParentGuardian>(),

                AcademicHistories = dto.AcademicHistories?.Select(h => new AcademicHistory
                {
                    Qualification = h.Qualification,
                    BoardOrUniversity = h.BoardOrUniversity,
                    InstitutionName = h.InstitutionName,
                    PassedYear = h.PassedYear,
                    DivisionOrGPA = h.DivisionOrGPA,
                    MarksheetPath = h.MarksheetPath,
                    CreatedOn = now,
                    UpdatedOn = now
                }).ToList() ?? new List<AcademicHistory>(),

                ExtracurricularDetails = dto.ExtracurricularDetails?.Select(e => new ExtracurricularDetail
                {
                    Interests = e.Interests,
                    Achievements = e.Achievements,
                    ScholarType = e.ScholarType,
                    TransportMethod = e.TransportMethod,
                    CreatedOn = now,
                    UpdatedOn = now
                }).ToList() ?? new List<ExtracurricularDetail>(),

                Documents = dto.Documents?.Select(doc => new StudentDocument
                {
                    DocumentType = doc.DocumentType,
                    FilePath = doc.FilePath,
                    CreatedOn = now,
                    UpdatedOn = now
                }).ToList() ?? new List<StudentDocument>()
            };

            // Single transaction - EF Core handles all relationships
            await _unitOfWork.Students.AddAsync(student);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteStudentAsync(Student student)
        {
            _unitOfWork.Students.Remove(student);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return (await _unitOfWork.Students.GetAllAsync()).ToList();
        }

        public async Task<Student> GetStudentByIdAsync(int id)  // Changed from GetStudentByPidAsync
        {
            return await _unitOfWork.Students.GetByIdAsync(id);  // Changed from GetByGuidAsync
        }

        public async Task UpdateStudentAsync(Student student)
        {
            _unitOfWork.Students.Update(student);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
