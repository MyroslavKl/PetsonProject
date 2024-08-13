using Application.DTOs.ImageDTOs;
using Application.Persistence.Repositories;
using Application.Persistence.Services;
using AutoMapper;
using CacheServices.Service;
using Domain.Entities;

namespace Infrastructure.Persistence.Services;

public class ImageService:IImageService
{
    private readonly IImageRepository _imageRepository;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;

    public ImageService(IImageRepository imageRepository,IMapper mapper,ICacheService cacheService)
    {
        _imageRepository = imageRepository;
        _mapper = mapper;
        _cacheService = cacheService;

    }
    public IEnumerable<ImageDto> GetAllImages(int petId) ////
    {
        var images = _imageRepository.GetAll(obj => obj.PetId == petId);
        var imagesDto = _mapper.Map<IEnumerable<ImageDto>>(images);
        return imagesDto;
    }

    public async Task AddImageAsync(UpsertImage imageDto)
    {
        var image = _mapper.Map<Image>(imageDto);
        await _imageRepository.InsertAsync(image);
        await _imageRepository.SaveChangesAsync();
        var dto = _mapper.Map<ImageDto>(image);
        var expirationTime = DateTime.Now.AddMinutes(3);
        _cacheService.SetData($"image{image.Id}",dto,expirationTime);
    }

    public async Task DeleteImageAsync(Image image)
    {
        _imageRepository.Delete(image);
        await _imageRepository.SaveChangesAsync();
        _cacheService.RemoveData($"image{image.Id}");
    }
}