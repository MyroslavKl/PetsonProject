using Application.DTOs.ReserveDTOs;
using Application.Persistence.Repositories;
using Application.Persistence.Services;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Persistence.Services;

public class ReserveService:IReserveService
{
    private readonly IReserveRepository _reserveRepository;
    private readonly IMapper _mapper;

    public ReserveService(IReserveRepository reserveRepository,IMapper mapper)
    {
        _reserveRepository = reserveRepository;
        _mapper = mapper;
    }
    public IEnumerable<ReserveDto> GetAllReserves(int userId)
    {
        var reserves = _reserveRepository.GetAllAsync(obj => obj.UserId == userId);
        var reservesDto = _mapper.Map<IEnumerable<ReserveDto>>(reserves);
        return reservesDto;
    }

    public async Task<IEnumerable<ReserveDto>> GetReservesByDate(DateTime date)
    {
        var reserves = _reserveRepository.GetAllAsync(obj => obj.ReserveDate == date);
        var reservesDto = _mapper.Map<IEnumerable<ReserveDto>>(reserves);
        return reservesDto;
    }
    

    public async Task DeleteReserve(Reserve reserve)
    {
        _reserveRepository.DeleteAsync(reserve);
        await _reserveRepository.SaveChangesAsync();
    }

    public async Task CreateReserve(UpsertReserveDto reserveDto)
    {
        var reserve = _mapper.Map<Reserve>(reserveDto);
        await _reserveRepository.InsertAsync(reserve);
        await _reserveRepository.SaveChangesAsync();
    }
}