using Application.DTOs.PetDTOs;
using Application.DTOs.ReserveDTOs;
using Domain.Entities;

namespace Application.Persistence.Services;

public interface IReserveService
{
    IEnumerable<ReserveDto> GetAllReserves(int userId);
    Task<IEnumerable<ReserveDto>> GetReservesByDate(DateOnly date);
    Task<IEnumerable<ReserveDto>> GetReservesBySpecies(string species);
    Task UpdateDateOfReserve(Reserve reserve);
    Task DeleteReserve(Reserve reserve);
    Task CreateReserve(UpsertReserveDto reserve);
}