using System.Linq.Expressions;

namespace Application.Persistence.Repositories.Common;

public interface IRepository<TEntity> where TEntity: class
{
    IEnumerable<TEntity> GetAllAsync(Expression<Func<TEntity,bool>>? filter = null);
    Task<TEntity?> GetOneAsync(Expression<Func<TEntity,bool>>? filter = null);
    Task InsertAsync(TEntity obj);
    void UpdateAsync(TEntity obj);
    void DeleteAsync(TEntity obj);
    Task SaveChangesAsync();
}