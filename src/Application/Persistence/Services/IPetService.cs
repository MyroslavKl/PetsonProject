using Application.DTOs.PetDTOs;
using Application.DTOs.UserDTOs;
using Domain.Entities;

namespace Application.Persistence.Services;

public interface IPetService
{
    IEnumerable<PetDto> GetAllUsers();
    Task<IEnumerable<PetDto>> GetPetBySpecies(string species);
    Task<IEnumerable<PetDto>> GetPetByType(string type);
    Task AddPet(UpsertPetDto pet);
    Task UpdatePetName(string petName,UpsertPetDto pet);
    Task UpdateDescription(string text,UpsertPetDto pet);
    Task DeletePetFromSite(int id);
}