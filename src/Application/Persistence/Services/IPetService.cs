using Application.DTOs.PetDTOs;
using Application.DTOs.UserDTOs;
using Domain.Entities;

namespace Application.Persistence.Services;

public interface IPetService
{
    IEnumerable<PetDto> GetAllPets();
    Task<IEnumerable<PetDto>> GetPetBySpecies(string species);
    Task<IEnumerable<PetDto>> GetPetByType(string type);
    Task AddPet(UpsertPetDto pet);
    Task UpdatePetName(string petName,Pet pet);
    Task UpdateDescription(string text,Pet pet);
    Task DeletePetFromSite(Pet pet);
}