using Application.DTOs.ReserveDTOs;
using Application.Persistence.Repositories;
using Application.Persistence.Services;
using Application.Persistence.Services.CacheService;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Persistence.Services;

public class ReserveService:IReserveService
{
    private readonly IReserveRepository _reserveRepository;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;

    public ReserveService(IReserveRepository reserveRepository,IMapper mapper,ICacheService cacheService)
    {
        _reserveRepository = reserveRepository;
        _mapper = mapper;
        _cacheService = cacheService;
    }
    public IEnumerable<ReserveDto> GetAllReserves(int userId)
    {
        var cacheData = _cacheService.GetData<IEnumerable<ReserveDto>>("reserve");
        if (cacheData != null && cacheData.Count() > 0)
        {
            return cacheData;
        }
        var reserves = _reserveRepository.GetAll(obj => obj.UserId == userId);
        cacheData = _mapper.Map<IEnumerable<ReserveDto>>(reserves);
        
        var expirationTime = DateTime.Now.AddMinutes(3);
        _cacheService.SetData("reserve",cacheData,expirationTime);
        
        return cacheData;
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
        _cacheService.RemoveData($"reserve{reserve.Id}");
    }

    public async Task CreateReserveAsync(UpsertReserveDto reserveDto)
    {
        var reserve = _mapper.Map<Reserve>(reserveDto);
        await _reserveRepository.InsertAsync(reserve);
        await _reserveRepository.SaveChangesAsync();
        var dto = _mapper.Map<ReserveDto>(reserve);
        var expirationTime = DateTime.Now.AddMinutes(3);
        _cacheService.SetData($"reserve{reserve.Id}",dto,expirationTime);
    }
}