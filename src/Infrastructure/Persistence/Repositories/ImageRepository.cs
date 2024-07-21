using Application.Persistence.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Repositories.Common;

namespace Infrastructure.Persistence.Repositories;

public class ImageRepository:Repository<Image>,IImageRepository
{
    public ImageRepository(PetsonContext dbContext) : base(dbContext)
    {
    }
}