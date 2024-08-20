using Application.DTOs.AuthDtos;
using Application.DTOs.UserDTOs;

namespace Application.Persistence.Services.AuthServices;

public interface IAuthService
{
    Task RegisterAsync(CreateUserDto createUserDto);
    Task<string?> LoginAsync(LoginDto loginDto);
}