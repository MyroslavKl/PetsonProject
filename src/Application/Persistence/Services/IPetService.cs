using Application.DTOs.ImageDTOs;
using Application.DTOs.PetDTOs;
using Application.DTOs.UserDTOs;
using Domain.Entities;

namespace Application.Persistence.Services;

public interface IPetService
{
    IEnumerable<PetDto> GetAllPets();
    Task<IEnumerable<PetDto>> GetPetBySpeciesAsync(string species);
    Task<IEnumerable<PetDto>> GetPetByTypeAsync(string type);
    Task AddPetAsync(UpsertPetDto pet);
    Task UpdatePetNameAsync(string petName,Pet pet);
    Task UpdateDescriptionAsync(string text,Pet pet);
    Task DeletePetFromSiteAsync(Pet pet);
    IEnumerable<ImageDto> GetAllImages(int id);
}