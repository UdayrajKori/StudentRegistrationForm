namespace StudentRegistrationForm.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public Guid Pid { get; set; } = Guid.NewGuid();
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public DateTime DeletedOn { get; set; }
    }
}
