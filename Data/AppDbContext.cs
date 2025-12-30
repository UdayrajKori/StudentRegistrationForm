using Microsoft.EntityFrameworkCore;
using StudentRegistrationForm.Models;

namespace StudentRegistrationForm.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSets for all entities
        public DbSet<Student> Students { get; set; }
        public DbSet<PersonalDetails> PersonalDetails { get; set; }
        public DbSet<ContactDetail> ContactDetails { get; set; }
        public DbSet<ParentGuardian> ParentGuardians { get; set; }
        public DbSet<EmergencyContact> EmergencyContacts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<DisabilityDetail> DisabilityDetails { get; set; }
        public DbSet<FinancialDetail> FinancialDetails { get; set; }
        public DbSet<BankDetail> BankDetails { get; set; }
        public DbSet<CitizenshipDetail> CitizenshipDetails { get; set; }
        public DbSet<AcademicHistory> AcademicHistories { get; set; }
        public DbSet<AcademicEnrollment> AcademicEnrollments { get; set; }
        public DbSet<ExtracurricularDetail> ExtracurricularDetails { get; set; }
        public DbSet<StudentDocument> StudentDocuments { get; set; }
        public DbSet<Declaration> Declarations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Student entity
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Pid).IsUnique();
                entity.Property(e => e.Pid).HasDefaultValueSql("NEWID()");
            });

            // Configure decimal precision for ScholarshipAmount
            modelBuilder.Entity<FinancialDetail>()
                .Property(f => f.ScholarshipAmount)
                .HasColumnType("decimal(18,2)"); // Max: 9,999,999,999,999,999.99

            // Configure relationships using Pid as foreign key
            ConfigureRelationship<PersonalDetails>(modelBuilder, "StudentPid");
            ConfigureRelationship<ContactDetail>(modelBuilder, "StudentPid");
            ConfigureRelationship<ParentGuardian>(modelBuilder, "StudentPid");
            ConfigureRelationship<EmergencyContact>(modelBuilder, "StudentPid");
            ConfigureRelationship<Address>(modelBuilder, "StudentPid");
            ConfigureRelationship<DisabilityDetail>(modelBuilder, "StudentPid");
            ConfigureRelationship<FinancialDetail>(modelBuilder, "StudentPid");
            ConfigureRelationship<BankDetail>(modelBuilder, "StudentPid");
            ConfigureRelationship<CitizenshipDetail>(modelBuilder, "StudentPid");
            ConfigureRelationship<AcademicHistory>(modelBuilder, "StudentPid");
            ConfigureRelationship<AcademicEnrollment>(modelBuilder, "StudentPid");
            ConfigureRelationship<ExtracurricularDetail>(modelBuilder, "StudentPid");
            ConfigureRelationship<StudentDocument>(modelBuilder, "StudentPid");
            ConfigureRelationship<Declaration>(modelBuilder, "StudentPid");

            base.OnModelCreating(modelBuilder);
        }

        private void ConfigureRelationship<TEntity>(ModelBuilder modelBuilder, string foreignKeyName)
            where TEntity : class
        {
            modelBuilder.Entity<TEntity>()
                .HasOne<Student>()
                .WithMany()
                .HasForeignKey(foreignKeyName)
                .HasPrincipalKey(s => s.Pid)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
