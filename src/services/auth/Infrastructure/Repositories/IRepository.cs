using Core.Entities;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task AddAsync(T entity, CancellationToken cancellationToken);
        Task UpdateAsync(T entity, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
