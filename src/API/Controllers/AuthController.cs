using Application.ActionFilters.AuthFilters;
using Application.DTOs.AuthDtos;
using Application.DTOs.UserDTOs;
using Application.Persistence.Services.AuthServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
    
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            return Ok(await _authService.LoginAsync(loginDto));
        }
        
        [HttpPost("register")]
        [TypeFilter(typeof(RegisterActionFilterAttribute))]
        public async Task Register([FromBody] CreateUserDto createUserDto)
        {
            await _authService.RegisterAsync(createUserDto);
        }
    }
}
