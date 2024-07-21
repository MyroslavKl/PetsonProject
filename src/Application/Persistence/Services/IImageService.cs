using Application.DTOs.ImageDTOs;

namespace Application.Persistence.Services;

public interface IImageService
{
    IEnumerable<ImageDto> GetAllImages(int id);
    Task AddImage(UpsertImage image);
    Task UpdateImage(string url, UpsertImage image);
    
}