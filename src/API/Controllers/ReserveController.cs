using Application.ActionFilters;
using Application.ActionFilters.ReserveFilters;
using Application.DTOs.ReserveDTOs;
using Application.Persistence.Repositories;
using Application.Persistence.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReserveController : ControllerBase
    {
        private readonly IReserveService _reserveService;
        private readonly IReserveRepository _reserveRepository;

        public ReserveController(IReserveService reserveService,IReserveRepository reserveRepository)
        {
            _reserveService = reserveService;
            _reserveRepository = reserveRepository;
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(ReserveExistByIdFilterAttribute))]
        public IEnumerable<ReserveDto> GetReserves([FromRoute]int id)
        {
            var reserves = _reserveService.GetAllReserves(id);
            return reserves;
        }

        [HttpGet("specific-date")]
        [TypeFilter(typeof(ReserveExistByDateFilterAttribute))]
        public async Task<IEnumerable<ReserveDto>> GetByDate(DateTime date)
        {
           var reserves = await _reserveService.GetReservesByDateAsync(date);
           return reserves;
        }

        [HttpPost]
        [ModelStateFilter]
        public async Task<IActionResult> CreateReserve([FromBody] UpsertReserveDto reserveDto)
        {
            await _reserveService.CreateReserveAsync(reserveDto);
            return Ok("Reserve is created successfully");
        }

        [HttpDelete("{id}")]
        [TypeFilter(typeof(ReserveExistByIdFilterAttribute))]
        public async Task<IActionResult> RemoveService([FromRoute]int id)
        {
            var reserve = await _reserveRepository.GetOneAsync(obj => obj.Id == id);
            await _reserveService.DeleteReserveAsync(reserve);
            return Ok("Reserve successfully deleted");
        }


    }
}
