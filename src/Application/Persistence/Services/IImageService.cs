using Application.DTOs.ImageDTOs;
using Domain.Entities;

namespace Application.Persistence.Services;

public interface IImageService
{
    IEnumerable<ImageDto> GetAllImages(int petId);
    Task AddImageAsync(UpsertImage image);
    Task UpdateImageAsync(string url, Image image);
    
}