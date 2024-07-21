﻿using System.Linq.Expressions;
using Application.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Common;

public class Repository<TEntity>:IRepository<TEntity> where TEntity: class
{
    private readonly PetsonContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(PetsonContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }
    public IEnumerable<TEntity> GetAllAsync()
    {
        IQueryable<TEntity> query = _dbSet;
        return query;
    }

    public async Task<TEntity?> GetOneAsync(Expression<Func<TEntity, bool>>? filter = null)
    {
        IQueryable<TEntity> query = _dbSet;
        if (filter is not null)
        {
            query = query.Where(filter);
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task InsertAsync(TEntity obj)
    {
        await _dbSet.AddAsync(obj);
    }

    public void UpdateAsync(TEntity obj)
    {
        _dbSet.Update(obj);
    }

    public void DeleteAsync(TEntity obj)
    {
        _dbSet.Remove(obj);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}