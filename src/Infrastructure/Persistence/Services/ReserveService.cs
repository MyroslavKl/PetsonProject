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
        var reserves = _reserveRepository.GetAll(obj => obj.UserId == userId);
        var reservesDto = _mapper.Map<IEnumerable<ReserveDto>>(reserves);
        return reservesDto;
    }

    public async Task<IEnumerable<ReserveDto>> GetReservesByDateAsync(DateTime date)
    {
        var reserves = _reserveRepository.GetAll(obj => obj.ReserveDate == date);
        var reservesDto = _mapper.Map<IEnumerable<ReserveDto>>(reserves);
        return reservesDto;
    }
    

    public async Task DeleteReserveAsync(Reserve reserve)
    {
        _reserveRepository.Delete(reserve);
        await _reserveRepository.SaveChangesAsync();
    }

    public async Task CreateReserveAsync(UpsertReserveDto reserveDto)
    {
        var reserve = _mapper.Map<Reserve>(reserveDto);
        await _reserveRepository.InsertAsync(reserve);
        await _reserveRepository.SaveChangesAsync();
    }
}