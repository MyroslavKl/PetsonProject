using Application.DTOs.ImageDTOs;
using Domain.Entities;

namespace Application.Persistence.Services;

public interface IImageService
{
    IEnumerable<ImageDto> GetAllImages(int petId);
    Task AddImage(UpsertImage image);
    Task UpdateImage(string url, Image image);
    
}