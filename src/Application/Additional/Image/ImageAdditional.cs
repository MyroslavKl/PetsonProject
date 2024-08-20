using Application.DTOs.ImageDTOs;
using Application.Persistence.Repositories;
using Application.Persistence.Services.CacheService;
using AutoMapper;

namespace Application.Additional.Image;

public class ImageAdditional:IImageAdditional
{
    private readonly IImageRepository _imageRepository;
    private readonly ICacheService _cacheService;
    private readonly IMapper _mapper;
    public Task<Domain.Entities.Image> AddImageAdditional(UpsertImage imageDto)
    {
        throw new NotImplementedException();
    }

    public void SetImageAdditional(Domain.Entities.Image image)
    {
        throw new NotImplementedException();
    }
}