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

            // Handle Multiple Addresses
            if (dto.Addresses != null && dto.Addresses.Any())
            {
                foreach (var addressDto in dto.Addresses)
                {
                    var address = new Address
                    {
                        StudentPid = studentPid,
                        AddressType = addressDto.AddressType,
                        Province = addressDto.Province,
                        District = addressDto.District,
                        Municipality = addressDto.Municipality,
                        WardNumber = addressDto.WardNumber,
                        ToleStreet = addressDto.ToleStreet,
                        HouseNumber = addressDto.HouseNumber,
                        CreatedOn = DateTime.UtcNow,
                        UpdatedOn = DateTime.UtcNow
                    };
                    await _unitOfWork.Addresses.AddAsync(address);
                }
            }

            // Handle Multiple Emergency Contacts
            if (dto.EmergencyContacts != null && dto.EmergencyContacts.Any())
            {
                foreach (var contactDto in dto.EmergencyContacts)
                {
                    var emergencyContact = new EmergencyContact
                    {
                        StudentPid = studentPid,
                        ContactName = contactDto.ContactName,
                        Relation = contactDto.Relation,
                        ContactNumber = contactDto.ContactNumber,
                        CreatedOn = DateTime.UtcNow,
                        UpdatedOn = DateTime.UtcNow
                    };
                    await _unitOfWork.EmergencyContacts.AddAsync(emergencyContact);
                }
            }

            // Handle Multiple Disability Details
            if (dto.DisabilityDetails != null && dto.DisabilityDetails.Any())
            {
                foreach (var disabilityDto in dto.DisabilityDetails)
                {
                    var disabilityDetail = new DisabilityDetail
                    {
                        StudentPid = studentPid,
                        DisabilityType = disabilityDto.DisabilityType,
                        DisabilityPercentage = disabilityDto.DisabilityPercentage,
                        CreatedOn = DateTime.UtcNow,
                        UpdatedOn = DateTime.UtcNow
                    };
                    await _unitOfWork.DisabilityDetails.AddAsync(disabilityDetail);
                }
            }

            // Handle Multiple Parent/Guardians
            if (dto.ParentGuardians != null && dto.ParentGuardians.Any())
            {
                foreach (var parentDto in dto.ParentGuardians)
                {
                    var parentGuardian = new ParentGuardian
                    {
                        StudentPid = studentPid,
                        ParentType = parentDto.ParentType,
                        FullName = parentDto.FullName,
                        Occupation = parentDto.Occupation,
                        Designation = parentDto.Designation,
                        Organization = parentDto.Organization,
                        MobileNumber = parentDto.MobileNumber,
                        GardianEmail = parentDto.GardianEmail,
                        AnnualFamilyIncome = parentDto.AnnualFamilyIncome,
                        CreatedOn = DateTime.UtcNow,
                        UpdatedOn = DateTime.UtcNow
                    };
                    await _unitOfWork.ParentGuardians.AddAsync(parentGuardian);
                }
            }

            // Handle Multiple Academic Histories
            if (dto.AcademicHistories != null && dto.AcademicHistories.Any())
            {
                foreach (var historyDto in dto.AcademicHistories)
                {
                    var academicHistory = new AcademicHistory
                    {
                        StudentPid = studentPid,
                        Qualification = historyDto.Qualification,
                        BoardOrUniversity = historyDto.BoardOrUniversity,
                        InstitutionName = historyDto.InstitutionName,
                        PassedYear = historyDto.PassedYear,
                        DivisionOrGPA = historyDto.DivisionOrGPA,
                        MarksheetPath = historyDto.MarksheetPath,
                        CreatedOn = DateTime.UtcNow,
                        UpdatedOn = DateTime.UtcNow
                    };
                    await _unitOfWork.AcademicHistories.AddAsync(academicHistory);
                }
            }

            // Handle Multiple Extracurricular Details
            if (dto.ExtracurricularDetails != null && dto.ExtracurricularDetails.Any())
            {
                foreach (var detail in dto.ExtracurricularDetails)
                {
                    var extracurricularDetail = new ExtracurricularDetail
                    {
                        StudentPid = studentPid,
                        Interests = detail.Interests,
                        Achievements = detail.Achievements,
                        ScholarType = detail.ScholarType,
                        TransportMethod = detail.TransportMethod,
                        CreatedOn = DateTime.UtcNow,
                        UpdatedOn = DateTime.UtcNow
                    };
                    await _unitOfWork.ExtracurricularDetails.AddAsync(extracurricularDetail);
                }
            }

            // Handle Multiple Documents
            if (dto.Documents != null && dto.Documents.Any())
            {
                foreach (var doc in dto.Documents)
                {
                    var studentDocument = new StudentDocument
                    {
                        StudentPid = studentPid,
                        DocumentType = doc.DocumentType,
                        FilePath = doc.FilePath,
                        CreatedOn = DateTime.UtcNow,
                        UpdatedOn = DateTime.UtcNow
                    };
                    await _unitOfWork.StudentDocuments.AddAsync(studentDocument);
                }
            }

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
            return await _unitOfWork.Students.GetByGuidAsync(pid);
        }

        public async Task UpdateStudentAsync(Student student)
        {
            _unitOfWork.Students.Update(student);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
