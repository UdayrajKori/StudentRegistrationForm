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
            // 1. Create Student with auto-generated Pid
            var student = new Student
            {
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                LastName = dto.LastName,
                DateOfBirth = dto.DateOfBirth,
                PlaceOfBirth = dto.PlaceOfBirth,
                PhotoPath = dto.PhotoPath,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };

            await _unitOfWork.Students.AddAsync(student);
            await _unitOfWork.SaveChangesAsync(); // Save to generate Pid

            var studentPid = student.Pid; // Get the generated Pid

            // 2. Create all related entities using StudentPid
            var personalDetails = new PersonalDetails
            {
                StudentPid = studentPid,
                Gender = dto.Gender,
                Nationality = dto.Nationality,
                BloodGroup = dto.BloodGroup,
                MaritalStatus = dto.MaritalStatus,
                Religion = dto.Religion,
                Ethnicity = dto.Ethnicity,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };
            await _unitOfWork.PersonalDetails.AddAsync(personalDetails);

            var contactDetail = new ContactDetail
            {
                StudentPid = studentPid,
                Email = dto.Email,
                AlternateEmail = dto.AlternateEmail,
                PrimaryMobile = dto.PrimaryMobile,
                SecondaryMobile = dto.SecondaryMobile,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };
            await _unitOfWork.ContactDetails.AddAsync(contactDetail);

            var parentGuardian = new ParentGuardian
            {
                StudentPid = studentPid,
                ParentType = dto.ParentType,
                FullName = dto.FullName,
                Occupation = dto.Occupation,
                Designation = dto.Designation,
                Organization = dto.Organization,
                MobileNumber = dto.MobileNumber,
                GardianEmail = dto.GardianEmail,
                AnnualFamilyIncome = dto.AnnualFamilyIncome,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };
            await _unitOfWork.ParentGuardians.AddAsync(parentGuardian);

            var emergencyContact = new EmergencyContact
            {
                StudentPid = studentPid,
                ContactName = dto.ContactName,
                Relation = dto.Relation,
                ContactNumber = dto.ContactNumber,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };
            await _unitOfWork.EmergencyContacts.AddAsync(emergencyContact);

            var address = new Address
            {
                StudentPid = studentPid,
                AddressType = dto.AddressType,
                Province = dto.Province,
                District = dto.District,
                Municipality = dto.Municipality,
                WardNumber = dto.WardNumber,
                ToleStreet = dto.ToleStreet,
                HouseNumber = dto.HouseNumber,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };
            await _unitOfWork.Addresses.AddAsync(address);

            var disabilityDetail = new DisabilityDetail
            {
                StudentPid = studentPid,
                DisabilityType = dto.DisabilityType,
                DisabilityPercentage = dto.DisabilityPercentage,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };
            await _unitOfWork.DisabilityDetails.AddAsync(disabilityDetail);

            var financialDetail = new FinancialDetail
            {
                StudentPid = studentPid,
                FeeCategory = dto.FeeCategory,
                ScholarshipType = dto.ScholarshipType,
                ScholarshipProviderName = dto.ScholarshipProviderName,
                ScholarshipAmount = dto.ScholarshipAmount,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };
            await _unitOfWork.FinancialDetails.AddAsync(financialDetail);

            var bankDetail = new BankDetail
            {
                StudentPid = studentPid,
                AccountHolderName = dto.AccountHolderName,
                BankName = dto.BankName,
                AccountNumber = dto.AccountNumber,
                Branch = dto.Branch,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };
            await _unitOfWork.BankDetails.AddAsync(bankDetail);

            var citizenshipDetail = new CitizenshipDetail
            {
                StudentPid = studentPid,
                CitizenshipNumber = dto.CitizenshipNumber,
                IssueDate = dto.IssueDate,
                IssueDistrict = dto.IssueDistrict,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };
            await _unitOfWork.CitizenshipDetails.AddAsync(citizenshipDetail);

            var academicHistory = new AcademicHistory
            {
                StudentPid = studentPid,
                Qualification = dto.Qualification,
                BoardOrUniversity = dto.BoardOrUniversity,
                InstitutionName = dto.InstitutionName,
                PassedYear = dto.PassedYear,
                DivisionOrGPA = dto.DivisionOrGPA,
                MarksheetPath = dto.MarksheetPath,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };
            await _unitOfWork.AcademicHistories.AddAsync(academicHistory);

            var academicEnrollment = new AcademicEnrollment
            {
                StudentPid = studentPid,
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
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };
            await _unitOfWork.AcademicEnrollments.AddAsync(academicEnrollment);

            var extracurricularDetail = new ExtracurricularDetail
            {
                StudentPid = studentPid,
                Interests = dto.Interests,
                Achievements = dto.Achievements,
                ScholarType = dto.ScholarType,
                TransportMethod = dto.TransportMethod,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };
            await _unitOfWork.ExtracurricularDetails.AddAsync(extracurricularDetail);

            var studentDocument = new StudentDocument
            {
                StudentPid = studentPid,
                DocumentType = dto.DocumentType,
                FilePath = dto.FilePath,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };
            await _unitOfWork.StudentDocuments.AddAsync(studentDocument);

            var declaration = new Declaration
            {
                StudentPid = studentPid,
                IsAgreed = dto.IsAgreed,
                ApplicationDate = dto.ApplicationDate,
                Place = dto.Place,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };
            await _unitOfWork.Declarations.AddAsync(declaration);

            // Final save for all related entities
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

        public async Task<Student> GetStudentByPidAsync(Guid pid)
        {
            // ✅ Fixed: Now uses GetByGuidAsync to match interface
            return await _unitOfWork.Students.GetByGuidAsync(pid);
        }

        public async Task UpdateStudentAsync(Student student)
        {
            _unitOfWork.Students.Update(student);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
