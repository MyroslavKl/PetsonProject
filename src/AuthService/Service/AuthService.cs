using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.DTOs.AuthDtos;
using Application.DTOs.UserDTOs;
using Application.Persistence.Repositories;
using Application.Persistence.Services.AuthServices;
using Application.Persistence.Services.HashServices;
using AutoMapper;
using CacheServices.Service;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Service;

public class AuthService : IAuthService
{
    private readonly IHashService _hashService;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly ICacheService _cacheService;

    public AuthService(IHashService hashService, IUserRepository userRepository, IMapper mapper,IConfiguration configuration,ICacheService cacheService)
    {
        _hashService = hashService;
        _userRepository = userRepository;
        _mapper = mapper;
        _configuration = configuration;
        _cacheService = cacheService;
    }

    public async Task RegisterAsync(CreateUserDto createUserDto)
    {
        var createUser = _mapper.Map<User>(createUserDto);
        createUser.Password = _hashService.HashPassword(createUser.Password);
        createUser.RoleId = 1;
        await _userRepository.InsertAsync(createUser);
        await _userRepository.SaveChangesAsync();
        Console.WriteLine($"user{createUser.Id}");
        var expirationTime = DateTime.Now.AddMinutes(3);
        _cacheService.SetData($"user{createUser.Id}",createUserDto,expirationTime);
    }

    public async Task<string> LoginAsync(LoginDto loginDto)
    {
        var user = await _userRepository.GetOneAsync(obj => obj.Email == loginDto.Email);
        if (user == null)
        {
            throw new Exception(message: "User Not Found");
        }

        var isVerified = _hashService.VerifyPassword(user.Password, loginDto.Password);
        if (!isVerified)
        {
            throw new Exception(message: "Not Valid Password");
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
        var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
        var claim = new[]
        {
            new Claim(ClaimTypes.Email,loginDto.Email)
        };
        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience:_configuration["JWT:Audience"],
            claims:claim,
            expires:DateTime.Now.AddMinutes(60),
            signingCredentials:credentials
            );
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}