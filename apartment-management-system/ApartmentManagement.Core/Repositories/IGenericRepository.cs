using System.Linq.Expressions;


namespace ApartmentManagement.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAll();
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}

