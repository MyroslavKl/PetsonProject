using Application.DTOs.AuthDtos;
using Microsoft.Extensions.Configuration;

namespace Application.Additional.Auth;

public interface IAuthAdditional
{
    string? JwtGenerator(IConfiguration configuration,LoginDto loginDto);
}