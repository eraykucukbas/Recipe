using System.Linq.Expressions;

namespace Recipe.Core.Interfaces.Services;

public interface IService<T> where T : class
{
    Task<T> CheckUserAccess(int id, string userId);
    Task<bool> EnsureExistsOrThrow(Expression<Func<T, bool>> expression);
    Task<bool> EnsureNotExistsOrThrow(Expression<Func<T, bool>> expression);
    Task<T> GetByIdAsync(int id);
    IQueryable<T> GetAll();
    IQueryable<T> Where(Expression<Func<T, bool>> expression);
    Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
    Task<T> AddAsync(T entity);
    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
    Task<T> UpdateAsync(T entity);
    Task RemoveAsync(T entity);
    Task RemoveRangeAsync(IEnumerable<T> entities);
}