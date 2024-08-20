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

    public ImageAdditional(IImageRepository imageRepository, ICacheService cacheService, IMapper mapper)
    {
        _cacheService = cacheService;
        _imageRepository = imageRepository;
        _mapper = mapper;
    }
    public async Task<Domain.Entities.Image> AddImageAdditional(UpsertImage imageDto)
    {
        var image = _mapper.Map<Domain.Entities.Image>(imageDto);
        await _imageRepository.InsertAsync(image);
        await _imageRepository.SaveChangesAsync();
        return image;
    }

    public void SetImageAdditional(Domain.Entities.Image image)
    {
        var dto = _mapper.Map<ImageDto>(image);
        var expirationTime = DateTime.Now.AddMinutes(3);
        _cacheService.SetData($"image{image.Id}",dto,expirationTime);
    }
}