using Application.DTOs.PetDTOs;
using Application.DTOs.ReserveDTOs;

namespace Application.Persistence.Services;

public interface IReserveService
{
    IEnumerable<ReserveDto> GetAllReserves();
    Task<IEnumerable<ReserveDto>> GetReservesByDate(DateTime date);
    Task<IEnumerable<ReserveDto>> GetReservesBySpecies(string species);
    Task UpdateDateOfReserve(UpsertReserveDto reserve);
    Task DeleteReserve();
    Task CreateReserve(UpsertReserveDto reserve);
}