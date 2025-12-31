using Microsoft.EntityFrameworkCore;
using StudentRegistrationForm.Data;
using StudentRegistrationForm.Interfaces;
using StudentRegistrationForm.Interfaces.RepositoryInterfaces;
using StudentRegistrationForm.Models;
using StudentRegistrationForm.Repositories;
using System.Data;

namespace StudentRegistrationForm.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork  // ✅ Remove explicit IAsyncDisposable, it's in IUnitOfWork
    {
        private readonly AppDbContext _context;
        private bool _disposed;

        // All repositories
        public IGenericRepository<Student> Students { get; private set; }
        public IGenericRepository<PersonalDetails> PersonalDetails { get; private set; }
        public IGenericRepository<ContactDetail> ContactDetails { get; private set; }
        public IGenericRepository<ParentGuardian> ParentGuardians { get; private set; }
        public IGenericRepository<EmergencyContact> EmergencyContacts { get; private set; }
        public IGenericRepository<Address> Addresses { get; private set; }
        public IGenericRepository<DisabilityDetail> DisabilityDetails { get; private set; }
        public IGenericRepository<FinancialDetail> FinancialDetails { get; private set; }
        public IGenericRepository<BankDetail> BankDetails { get; private set; }
        public IGenericRepository<CitizenshipDetail> CitizenshipDetails { get; private set; }
        public IGenericRepository<AcademicHistory> AcademicHistories { get; private set; }
        public IGenericRepository<AcademicEnrollment> AcademicEnrollments { get; private set; }
        public IGenericRepository<ExtracurricularDetail> ExtracurricularDetails { get; private set; }
        public IGenericRepository<StudentDocument> StudentDocuments { get; private set; }
        public IGenericRepository<Declaration> Declarations { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            // Initialize all repositories
            Students = new GenericRepository<Student>(_context);
            PersonalDetails = new GenericRepository<PersonalDetails>(_context);
            ContactDetails = new GenericRepository<ContactDetail>(_context);
            ParentGuardians = new GenericRepository<ParentGuardian>(_context);
            EmergencyContacts = new GenericRepository<EmergencyContact>(_context);
            Addresses = new GenericRepository<Address>(_context);
            DisabilityDetails = new GenericRepository<DisabilityDetail>(_context);
            FinancialDetails = new GenericRepository<FinancialDetail>(_context);
            BankDetails = new GenericRepository<BankDetail>(_context);
            CitizenshipDetails = new GenericRepository<CitizenshipDetail>(_context);
            AcademicHistories = new GenericRepository<AcademicHistory>(_context);
            AcademicEnrollments = new GenericRepository<AcademicEnrollment>(_context);
            ExtracurricularDetails = new GenericRepository<ExtracurricularDetail>(_context);
            StudentDocuments = new GenericRepository<StudentDocument>(_context);
            Declarations = new GenericRepository<Declaration>(_context);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            var transaction = _context.Database.CurrentTransaction;
            if (transaction != null)
            {
                await transaction.CommitAsync(cancellationToken);
            }
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            var transaction = _context.Database.CurrentTransaction;
            if (transaction != null)
            {
                await transaction.RollbackAsync(cancellationToken);
            }
        }

        public async Task ExecuteTransactionAsync(Action action, CancellationToken cancellationToken = default)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                action();
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task ExecuteTransactionAsync(Func<Task> action, CancellationToken cancellationToken = default)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await action();
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        // IAsyncDisposable implementation
        public async ValueTask DisposeAsync()
        {
            await DisposeAsyncCore();
            Dispose(false);
            GC.SuppressFinalize(this);
        }

        protected virtual async ValueTask DisposeAsyncCore()
        {
            if (!_disposed)
            {
                if (_context.Database.GetDbConnection().State == ConnectionState.Open)
                {
                    await _context.DisposeAsync();
                }
            }
        }

        // IDisposable implementation
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }
}
