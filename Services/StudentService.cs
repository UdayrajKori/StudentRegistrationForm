using StudentRegistrationForm.DTOs.RequestDTOs;
using StudentRegistrationForm.DTOs.ResponseDTOs;
using StudentRegistrationForm.Interfaces;
using StudentRegistrationForm.Interfaces.ServiceInterface;
using StudentRegistrationForm.Models;
using Microsoft.EntityFrameworkCore;

namespace StudentRegistrationForm.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CompleteResponseDTO> AddStudentAsync(CompleteRequestDTO dto)
        {
            var now = DateTime.UtcNow;

            var student = new Student
            {
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                LastName = dto.LastName,
                DateOfBirth = dto.DateOfBirth,
                PlaceOfBirth = dto.PlaceOfBirth,
                PhotoPath = dto.PhotoPath,
                CreatedOn = now,
                UpdatedOn = now,

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

            await _unitOfWork.Students.AddAsync(student);
            await _unitOfWork.SaveChangesAsync();

            // Return response using internal Id (will be converted to Pid in DTO)
            return await GetStudentByIdAsync(student.Id);
        }

        // Internal method - uses Id
        public async Task<CompleteResponseDTO> GetStudentByIdAsync(int id)
        {
            var student = await _unitOfWork.Students.GetQueryable()
                .Include(s => s.PersonalDetails)
                .Include(s => s.ContactDetail)
                .Include(s => s.FinancialDetail)
                .Include(s => s.BankDetail)
                .Include(s => s.CitizenshipDetail)
                .Include(s => s.AcademicEnrollment)
                .Include(s => s.Declaration)
                .Include(s => s.Addresses)
                .Include(s => s.EmergencyContacts)
                .Include(s => s.DisabilityDetails)
                .Include(s => s.ParentGuardians)
                .Include(s => s.AcademicHistories)
                .Include(s => s.ExtracurricularDetails)
                .Include(s => s.Documents)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
                throw new KeyNotFoundException($"Student not found");

            return MapToResponseDTO(student);
        }

        // Public API method - uses Pid
        public async Task<CompleteResponseDTO> GetStudentByPidAsync(Guid pid)
        {
            var student = await _unitOfWork.Students.GetQueryable()
                .Include(s => s.PersonalDetails)
                .Include(s => s.ContactDetail)
                .Include(s => s.FinancialDetail)
                .Include(s => s.BankDetail)
                .Include(s => s.CitizenshipDetail)
                .Include(s => s.AcademicEnrollment)
                .Include(s => s.Declaration)
                .Include(s => s.Addresses)
                .Include(s => s.EmergencyContacts)
                .Include(s => s.DisabilityDetails)
                .Include(s => s.ParentGuardians)
                .Include(s => s.AcademicHistories)
                .Include(s => s.ExtracurricularDetails)
                .Include(s => s.Documents)
                .FirstOrDefaultAsync(s => s.Pid == pid);

            if (student == null)
                throw new KeyNotFoundException($"Student with Pid {pid} not found");

            return MapToResponseDTO(student);
        }

        public async Task<List<CompleteResponseDTO>> GetAllStudentsAsync()
        {
            var students = await _unitOfWork.Students.GetQueryable()
                .Include(s => s.PersonalDetails)
                .Include(s => s.ContactDetail)
                .Include(s => s.FinancialDetail)
                .Include(s => s.BankDetail)
                .Include(s => s.CitizenshipDetail)
                .Include(s => s.AcademicEnrollment)
                .Include(s => s.Declaration)
                .Include(s => s.Addresses)
                .Include(s => s.EmergencyContacts)
                .Include(s => s.DisabilityDetails)
                .Include(s => s.ParentGuardians)
                .Include(s => s.AcademicHistories)
                .Include(s => s.ExtracurricularDetails)
                .Include(s => s.Documents)
                .ToListAsync();

            return students.Select(MapToResponseDTO).ToList();
        }

        public async Task DeleteStudentAsync(Guid pid)
        {
            var student = await _unitOfWork.Students.GetQueryable()
                .FirstOrDefaultAsync(s => s.Pid == pid);

            if (student == null)
                throw new KeyNotFoundException($"Student with Pid {pid} not found");

            _unitOfWork.Students.Remove(student);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<CompleteResponseDTO> UpdateStudentAsync(Guid pid, CompleteRequestDTO dto)
        {
            var student = await _unitOfWork.Students.GetQueryable()
                .Include(s => s.PersonalDetails)
                .Include(s => s.ContactDetail)
                .Include(s => s.FinancialDetail)
                .Include(s => s.BankDetail)
                .Include(s => s.CitizenshipDetail)
                .Include(s => s.AcademicEnrollment)
                .Include(s => s.Declaration)
                .Include(s => s.Addresses)
                .Include(s => s.EmergencyContacts)
                .Include(s => s.DisabilityDetails)
                .Include(s => s.ParentGuardians)
                .Include(s => s.AcademicHistories)
                .Include(s => s.ExtracurricularDetails)
                .Include(s => s.Documents)
                .FirstOrDefaultAsync(s => s.Pid == pid);

            if (student == null)
                throw new KeyNotFoundException($"Student with Pid {pid} not found");

            var now = DateTime.UtcNow;

            // Update main student fields
            student.FirstName = dto.FirstName;
            student.MiddleName = dto.MiddleName;
            student.LastName = dto.LastName;
            student.DateOfBirth = dto.DateOfBirth;
            student.PlaceOfBirth = dto.PlaceOfBirth;
            student.PhotoPath = dto.PhotoPath;
            student.UpdatedOn = now;

            // Update PersonalDetails
            if (student.PersonalDetails != null)
            {
                student.PersonalDetails.Gender = dto.Gender;
                student.PersonalDetails.Nationality = dto.Nationality;
                student.PersonalDetails.BloodGroup = dto.BloodGroup;
                student.PersonalDetails.MaritalStatus = dto.MaritalStatus;
                student.PersonalDetails.Religion = dto.Religion;
                student.PersonalDetails.Ethnicity = dto.Ethnicity;
                student.PersonalDetails.UpdatedOn = now;
            }

            // Update ContactDetail
            if (student.ContactDetail != null)
            {
                student.ContactDetail.Email = dto.Email;
                student.ContactDetail.AlternateEmail = dto.AlternateEmail;
                student.ContactDetail.PrimaryMobile = dto.PrimaryMobile;
                student.ContactDetail.SecondaryMobile = dto.SecondaryMobile;
                student.ContactDetail.UpdatedOn = now;
            }

            // Update FinancialDetail
            if (student.FinancialDetail != null)
            {
                student.FinancialDetail.FeeCategory = dto.FeeCategory;
                student.FinancialDetail.ScholarshipType = dto.ScholarshipType;
                student.FinancialDetail.ScholarshipProviderName = dto.ScholarshipProviderName;
                student.FinancialDetail.ScholarshipAmount = dto.ScholarshipAmount;
                student.FinancialDetail.UpdatedOn = now;
            }

            // Update BankDetail
            if (student.BankDetail != null)
            {
                student.BankDetail.AccountHolderName = dto.AccountHolderName;
                student.BankDetail.BankName = dto.BankName;
                student.BankDetail.AccountNumber = dto.AccountNumber;
                student.BankDetail.Branch = dto.Branch;
                student.BankDetail.UpdatedOn = now;
            }

            // Update CitizenshipDetail
            if (student.CitizenshipDetail != null)
            {
                student.CitizenshipDetail.CitizenshipNumber = dto.CitizenshipNumber;
                student.CitizenshipDetail.IssueDate = dto.IssueDate;
                student.CitizenshipDetail.IssueDistrict = dto.IssueDistrict;
                student.CitizenshipDetail.UpdatedOn = now;
            }

            // Update AcademicEnrollment
            if (student.AcademicEnrollment != null)
            {
                student.AcademicEnrollment.Faculty = dto.Faculty;
                student.AcademicEnrollment.Program = dto.Program;
                student.AcademicEnrollment.Level = dto.Level;
                student.AcademicEnrollment.AcademicYear = dto.AcademicYear;
                student.AcademicEnrollment.Semester = dto.Semester;
                student.AcademicEnrollment.Section = dto.Section;
                student.AcademicEnrollment.RollNumber = dto.RollNumber;
                student.AcademicEnrollment.RegistrationNumber = dto.RegistrationNumber;
                student.AcademicEnrollment.EnrollmentDate = dto.EnrollmentDate;
                student.AcademicEnrollment.AcademicStatus = dto.AcademicStatus;
                student.AcademicEnrollment.UpdatedOn = now;
            }

            // Update Declaration
            if (student.Declaration != null)
            {
                student.Declaration.IsAgreed = dto.IsAgreed;
                student.Declaration.ApplicationDate = dto.ApplicationDate;
                student.Declaration.Place = dto.Place;
                student.Declaration.UpdatedOn = now;
            }

            // Update collection properties - clear existing and add new ones
            student.Addresses?.Clear();
            if (dto.Addresses != null)
            {
                foreach (var addressDto in dto.Addresses)
                {
                    student.Addresses.Add(new Address
                    {
                        StudentId = student.Id,
                        AddressType = addressDto.AddressType,
                        Province = addressDto.Province,
                        District = addressDto.District,
                        Municipality = addressDto.Municipality,
                        WardNumber = addressDto.WardNumber,
                        ToleStreet = addressDto.ToleStreet,
                        HouseNumber = addressDto.HouseNumber,
                        CreatedOn = now,
                        UpdatedOn = now
                    });
                }
            }

            student.EmergencyContacts?.Clear();
            if (dto.EmergencyContacts != null)
            {
                foreach (var contactDto in dto.EmergencyContacts)
                {
                    student.EmergencyContacts.Add(new EmergencyContact
                    {
                        StudentId = student.Id,
                        ContactName = contactDto.ContactName,
                        Relation = contactDto.Relation,
                        ContactNumber = contactDto.ContactNumber,
                        CreatedOn = now,
                        UpdatedOn = now
                    });
                }
            }

            student.DisabilityDetails?.Clear();
            if (dto.DisabilityDetails != null)
            {
                foreach (var disabilityDto in dto.DisabilityDetails)
                {
                    student.DisabilityDetails.Add(new DisabilityDetail
                    {
                        StudentId = student.Id,
                        DisabilityType = disabilityDto.DisabilityType,
                        DisabilityPercentage = disabilityDto.DisabilityPercentage,
                        CreatedOn = now,
                        UpdatedOn = now
                    });
                }
            }

            student.ParentGuardians?.Clear();
            if (dto.ParentGuardians != null)
            {
                foreach (var parentDto in dto.ParentGuardians)
                {
                    student.ParentGuardians.Add(new ParentGuardian
                    {
                        StudentId = student.Id,
                        ParentType = parentDto.ParentType,
                        FullName = parentDto.FullName,
                        Occupation = parentDto.Occupation,
                        Designation = parentDto.Designation,
                        Organization = parentDto.Organization,
                        MobileNumber = parentDto.MobileNumber,
                        GardianEmail = parentDto.GardianEmail,
                        AnnualFamilyIncome = parentDto.AnnualFamilyIncome,
                        CreatedOn = now,
                        UpdatedOn = now
                    });
                }
            }

            student.AcademicHistories?.Clear();
            if (dto.AcademicHistories != null)
            {
                foreach (var historyDto in dto.AcademicHistories)
                {
                    student.AcademicHistories.Add(new AcademicHistory
                    {
                        StudentId = student.Id,
                        Qualification = historyDto.Qualification,
                        BoardOrUniversity = historyDto.BoardOrUniversity,
                        InstitutionName = historyDto.InstitutionName,
                        PassedYear = historyDto.PassedYear,
                        DivisionOrGPA = historyDto.DivisionOrGPA,
                        MarksheetPath = historyDto.MarksheetPath,
                        CreatedOn = now,
                        UpdatedOn = now
                    });
                }
            }

            student.ExtracurricularDetails?.Clear();
            if (dto.ExtracurricularDetails != null)
            {
                foreach (var extraDto in dto.ExtracurricularDetails)
                {
                    student.ExtracurricularDetails.Add(new ExtracurricularDetail
                    {
                        StudentId = student.Id,
                        Interests = extraDto.Interests,
                        Achievements = extraDto.Achievements,
                        ScholarType = extraDto.ScholarType,
                        TransportMethod = extraDto.TransportMethod,
                        CreatedOn = now,
                        UpdatedOn = now
                    });
                }
            }

            student.Documents?.Clear();
            if (dto.Documents != null)
            {
                foreach (var docDto in dto.Documents)
                {
                    student.Documents.Add(new StudentDocument
                    {
                        StudentId = student.Id,
                        DocumentType = docDto.DocumentType,
                        FilePath = docDto.FilePath,
                        CreatedOn = now,
                        UpdatedOn = now
                    });
                }
            }

            _unitOfWork.Students.Update(student);
            await _unitOfWork.SaveChangesAsync();

            return await GetStudentByIdAsync(student.Id);
        }

        // Helper method - maps Entity to ResponseDTO with Pid
        private CompleteResponseDTO MapToResponseDTO(Student student)
        {
            return new CompleteResponseDTO
            {
                Pid = student.Pid,  // External: Use Pid (GUID)
                FirstName = student.FirstName,
                MiddleName = student.MiddleName,
                LastName = student.LastName,
                DateOfBirth = student.DateOfBirth,
                PlaceOfBirth = student.PlaceOfBirth,
                PhotoPath = student.PhotoPath,
                IsActive = student.IsActive,
                CreatedOn = student.CreatedOn,
                UpdatedOn = student.UpdatedOn,

                PersonalDetails = student.PersonalDetails != null ? new PersonalDetailsResponseDTO
                {
                    Pid = student.PersonalDetails.Pid,  // Use Pid
                    Gender = student.PersonalDetails.Gender,
                    Nationality = student.PersonalDetails.Nationality,
                    BloodGroup = student.PersonalDetails.BloodGroup,
                    MaritalStatus = student.PersonalDetails.MaritalStatus,
                    Religion = student.PersonalDetails.Religion,
                    Ethnicity = student.PersonalDetails.Ethnicity
                } : null,

                ContactDetail = student.ContactDetail != null ? new ContactDetailResponseDTO
                {
                    Pid = student.ContactDetail.Pid,
                    Email = student.ContactDetail.Email,
                    AlternateEmail = student.ContactDetail.AlternateEmail,
                    PrimaryMobile = student.ContactDetail.PrimaryMobile,
                    SecondaryMobile = student.ContactDetail.SecondaryMobile
                } : null,

                FinancialDetail = student.FinancialDetail != null ? new FinancialDetailResponseDTO
                {
                    Pid = student.FinancialDetail.Pid,
                    FeeCategory = student.FinancialDetail.FeeCategory,
                    ScholarshipType = student.FinancialDetail.ScholarshipType,
                    ScholarshipProviderName = student.FinancialDetail.ScholarshipProviderName,
                    ScholarshipAmount = student.FinancialDetail.ScholarshipAmount
                } : null,

                BankDetail = student.BankDetail != null ? new BankDetailResponseDTO
                {
                    Pid = student.BankDetail.Pid,
                    AccountHolderName = student.BankDetail.AccountHolderName,
                    BankName = student.BankDetail.BankName,
                    AccountNumber = student.BankDetail.AccountNumber,
                    Branch = student.BankDetail.Branch
                } : null,

                CitizenshipDetail = student.CitizenshipDetail != null ? new CitizenshipDetailResponseDTO
                {
                    Pid = student.CitizenshipDetail.Pid,
                    CitizenshipNumber = student.CitizenshipDetail.CitizenshipNumber,
                    IssueDate = student.CitizenshipDetail.IssueDate,
                    IssueDistrict = student.CitizenshipDetail.IssueDistrict
                } : null,

                AcademicEnrollment = student.AcademicEnrollment != null ? new AcademicEnrollmentResponseDTO
                {
                    Pid = student.AcademicEnrollment.Pid,
                    Faculty = student.AcademicEnrollment.Faculty,
                    Program = student.AcademicEnrollment.Program,
                    Level = student.AcademicEnrollment.Level,
                    AcademicYear = student.AcademicEnrollment.AcademicYear,
                    Semester = student.AcademicEnrollment.Semester,
                    Section = student.AcademicEnrollment.Section,
                    RollNumber = student.AcademicEnrollment.RollNumber,
                    RegistrationNumber = student.AcademicEnrollment.RegistrationNumber,
                    EnrollmentDate = student.AcademicEnrollment.EnrollmentDate,
                    AcademicStatus = student.AcademicEnrollment.AcademicStatus
                } : null,

                Declaration = student.Declaration != null ? new DeclarationResponseDTO
                {
                    Pid = student.Declaration.Pid,
                    IsAgreed = student.Declaration.IsAgreed,
                    ApplicationDate = student.Declaration.ApplicationDate,
                    Place = student.Declaration.Place
                } : null,

                Addresses = student.Addresses?.Select(a => new AddressResponseDTO
                {
                    Pid = a.Pid,
                    AddressType = a.AddressType,
                    Province = a.Province,
                    District = a.District,
                    Municipality = a.Municipality,
                    WardNumber = a.WardNumber,
                    ToleStreet = a.ToleStreet,
                    HouseNumber = a.HouseNumber
                }).ToList(),

                EmergencyContacts = student.EmergencyContacts?.Select(c => new EmergencyContactResponseDTO
                {
                    Pid = c.Pid,
                    ContactName = c.ContactName,
                    Relation = c.Relation,
                    ContactNumber = c.ContactNumber
                }).ToList(),

                DisabilityDetails = student.DisabilityDetails?.Select(d => new DisabilityDetailResponseDTO
                {
                    Pid = d.Pid,
                    DisabilityType = d.DisabilityType,
                    DisabilityPercentage = d.DisabilityPercentage
                }).ToList(),

                ParentGuardians = student.ParentGuardians?.Select(p => new ParentGuardianResponseDTO
                {
                    Pid = p.Pid,
                    ParentType = p.ParentType,
                    FullName = p.FullName,
                    Occupation = p.Occupation,
                    Designation = p.Designation,
                    Organization = p.Organization,
                    MobileNumber = p.MobileNumber,
                    GardianEmail = p.GardianEmail,
                    AnnualFamilyIncome = p.AnnualFamilyIncome
                }).ToList(),

                AcademicHistories = student.AcademicHistories?.Select(h => new AcademicHistoryResponseDTO
                {
                    Pid = h.Pid,
                    Qualification = h.Qualification,
                    BoardOrUniversity = h.BoardOrUniversity,
                    InstitutionName = h.InstitutionName,
                    PassedYear = h.PassedYear,
                    DivisionOrGPA = h.DivisionOrGPA,
                    MarksheetPath = h.MarksheetPath
                }).ToList(),

                ExtracurricularDetails = student.ExtracurricularDetails?.Select(e => new ExtracurricularDetailResponseDTO
                {
                    Pid = e.Pid,
                    Interests = e.Interests,
                    Achievements = e.Achievements,
                    ScholarType = e.ScholarType,
                    TransportMethod = e.TransportMethod
                }).ToList(),

                Documents = student.Documents?.Select(doc => new StudentDocumentResponseDTO
                {
                    Pid = doc.Pid,
                    DocumentType = doc.DocumentType,
                    FilePath = doc.FilePath
                }).ToList()
            };
        }
    }
}
