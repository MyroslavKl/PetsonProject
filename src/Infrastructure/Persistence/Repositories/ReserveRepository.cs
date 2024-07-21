using Application.Persistence.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Repositories.Common;

namespace Infrastructure.Persistence.Repositories;

public class ReserveRepository:Repository<Reserve>,IReserveRepository
{
    public ReserveRepository(PetsonContext dbContext) : base(dbContext)
    {
    }
}