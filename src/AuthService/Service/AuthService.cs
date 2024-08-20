using Application.Additional.Auth;
using Application.DTOs.AuthDtos;
using Application.DTOs.UserDTOs;
using Application.Persistence.Repositories;
using Application.Persistence.Services.AuthServices;
using Application.Persistence.Services.CacheService;
using Application.Persistence.Services.HashServices;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace AuthService.Service;

public class AuthService : IAuthService
{
    private readonly IHashService _hashService;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly ICacheService _cacheService;
    private readonly IAuthAdditional _authAdditional;

    public AuthService(IHashService hashService, 
        IUserRepository userRepository, 
        IMapper mapper,
        IConfiguration configuration,
        ICacheService cacheService,
        IAuthAdditional authAdditional)
    {
        _hashService = hashService;
        _userRepository = userRepository;
        _mapper = mapper;
        _configuration = configuration;
        _cacheService = cacheService;
        _authAdditional = authAdditional;
    }

    public async Task RegisterAsync(CreateUserDto createUserDto)
    {
        var createUser = _mapper.Map<User>(createUserDto);
        createUser.Password = _hashService.HashPassword(createUser.Password);
        createUser.RoleId = 1;
        var userDto = _mapper.Map<UserDto>(createUser);
        var expirationTime = DateTime.Now.AddMinutes(3);
        await _userRepository.InsertAsync(createUser);
        await _userRepository.SaveChangesAsync();
        userDto.Id = createUser.Id;
        _cacheService.SetData($"user{createUser.Id}",userDto,expirationTime);
    }

    public async Task<string?> LoginAsync(LoginDto loginDto)
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

        var jwt = _authAdditional.JwtGenerator(_configuration,loginDto);
        return jwt;
    }
}