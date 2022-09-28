using System.Linq.Expressions;
using Core.Domain;

namespace Core.Data;

public interface IRepository<T> where T : BaseEntity
{
    Task<List<T>> GetAll();

    Task<T?> FindAsync(Expression<Func<T, bool>> predicate);

    IQueryable<T> Find(Expression<Func<T, bool>> predicate);

    Task<List<T>> FindAllAsync(Expression<Func<T, bool>> predicate);

    Task<T?> GetByIdAsync(Guid id);

    IQueryable<T> FindById(Guid id);

    Task AddAsync(T entity);

    Task UpdateAsync(T entity);

    Task DeleteAsync(Guid id);

    Task UpdateRangeAsync(List<T> entities);


}