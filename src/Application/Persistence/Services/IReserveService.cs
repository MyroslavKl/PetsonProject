using Application.DTOs.PetDTOs;
using Application.DTOs.ReserveDTOs;
using Domain.Entities;

namespace Application.Persistence.Services;

public interface IReserveService
{
    IEnumerable<ReserveDto> GetAllReserves(int userId);
    Task<IEnumerable<ReserveDto>> GetReservesByDate(DateTime date);
    Task DeleteReserve(Reserve reserve);
    Task CreateReserve(UpsertReserveDto reserve);
}