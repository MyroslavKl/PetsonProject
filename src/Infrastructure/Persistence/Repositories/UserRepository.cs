using Application.Persistence.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Repositories.Common;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository:Repository<User>,IUserRepository
{
    public UserRepository(PetsonContext dbContext) : base(dbContext)
    {
    }
}