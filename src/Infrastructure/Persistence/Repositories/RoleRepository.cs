using Application.Persistence.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Repositories.Common;

namespace Infrastructure.Persistence.Repositories;

public class RoleRepository:Repository<Role>,IRoleRepository
{
    public RoleRepository(PetsonContext dbContext) : base(dbContext)
    {
    }
}