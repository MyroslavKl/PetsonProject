using Application.DTOs.ImageDTOs;

namespace Application.Additional.Image;

public interface IImageAdditional
{
    Task<Domain.Entities.Image> AddImageAdditional(UpsertImage imageDto);
    void SetImageAdditional(Domain.Entities.Image image);
    Task DeleteImageAdditional(Domain.Entities.Image image);
}