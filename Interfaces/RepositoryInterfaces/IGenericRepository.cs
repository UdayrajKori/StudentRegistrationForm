using System.Linq.Expressions;

namespace StudentRegistrationForm.Interfaces.RepositoryInterfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        //Task<T> GetByIdAsync(int id);
        Task<T> GetByGuidAsync(Guid pid);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
