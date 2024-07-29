using System.Linq.Expressions;
using Application.Persistence.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Repositories.Common;

namespace Infrastructure.Persistence.Repositories;

public class PetRepository:Repository<Pet>,IPetRepository
{
    private readonly PetsonContext _context;
    public PetRepository(PetsonContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }
    

    public async Task<IEnumerable<Pet>> GetOneOrManyPets(Expression<Func<Pet, bool>>? filter = null)
    {
        IQueryable<Pet> query = _context.Pets;
        if (filter is not null)
        {
            query = query.Where(filter);
        }

        return query;
    }
}