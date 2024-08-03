using Application.DTOs.AuthDtos;
using Application.DTOs.UserDTOs;

namespace Application.Persistence.Services.AuthServices;

public interface IAuthService
{
    Task Register(CreateUserDto createUserDto);
    Task<string> Login(LoginDto loginDto);
}