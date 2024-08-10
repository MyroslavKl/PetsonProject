using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application.Persistence.Repositories.Common;

public interface IRepository<TEntity> where TEntity: class
{
    IEnumerable<TEntity> GetAll(Expression<Func<TEntity,bool>>? filter = null);
    Task<TEntity?> GetOneAsync(Expression<Func<TEntity,bool>>? filter = null);
    Task InsertAsync(TEntity obj);
    void Update(TEntity obj);
    void Delete(TEntity obj);
    Task SaveChangesAsync();
}