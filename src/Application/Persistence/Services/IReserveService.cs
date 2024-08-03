using Application.DTOs.PetDTOs;
using Application.DTOs.ReserveDTOs;
using Domain.Entities;

namespace Application.Persistence.Services;

public interface IReserveService
{
    IEnumerable<ReserveDto> GetAllReserves(int userId);
    Task<IEnumerable<ReserveDto>> GetReservesByDateAsync(DateTime date);
    Task DeleteReserveAsync(Reserve reserve);
    Task CreateReserveAsync(UpsertReserveDto reserve);
}