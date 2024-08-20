using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.DTOs.AuthDtos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Additional.Auth;

public class AuthAdditional:IAuthAdditional
{
    public string? JwtGenerator(IConfiguration configuration,LoginDto loginDto)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
        var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
        var claim = new[]
        {
            new Claim(ClaimTypes.Email,loginDto.Email)
        };
        var token = new JwtSecurityToken(
            issuer: configuration["JWT:Issuer"],
            audience:configuration["JWT:Audience"],
            claims:claim,
            expires:DateTime.Now.AddMinutes(60),
            signingCredentials:credentials
        );
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}