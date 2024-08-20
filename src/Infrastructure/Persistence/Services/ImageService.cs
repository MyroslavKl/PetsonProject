using Application.Additional.Image;
using Application.DTOs.ImageDTOs;
using Application.Persistence.Repositories;
using Application.Persistence.Services;
using Application.Persistence.Services.CacheService;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Persistence.Services;

public class ImageService:IImageService
{
    private readonly IImageRepository _imageRepository;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;
    private readonly IImageAdditional _imageAdditional;

    public ImageService(IImageRepository imageRepository,
        IMapper mapper,
        ICacheService cacheService,
        IImageAdditional imageAdditional)
    {
        _imageRepository = imageRepository;
        _mapper = mapper;
        _cacheService = cacheService;
        _imageAdditional = imageAdditional;

    }
    public IEnumerable<ImageDto> GetAllImages(int petId) ////
    {
        var images = _imageRepository.GetAll(obj => obj.PetId == petId);
        var imagesDto = _mapper.Map<IEnumerable<ImageDto>>(images);
        return imagesDto;
    }

    public async Task AddImageAsync(UpsertImage imageDto)
    {
        var image = await _imageAdditional.AddImageAdditional(imageDto);
        _imageAdditional.SetImageAdditional(image);
    }

    public async Task DeleteImageAsync(Image image)
    {
        await _imageAdditional.DeleteImageAdditional(image);
        _cacheService.RemoveData($"image{image.Id}");
    }
}