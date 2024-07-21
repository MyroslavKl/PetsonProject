using Application.DTOs.AuthDtos;
using Application.DTOs.UserDTOs;
using Application.Persistence.Repositories;
using Application.Persistence.Services.AuthServices;
using Application.Persistence.Services.HashServices;
using AutoMapper;
using Domain.Entities;

namespace AuthService.Service;

public class AuthService : IAuthService
{
    private readonly IHashService _hashService;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public AuthService(IHashService hashService, IUserRepository userRepository, IMapper mapper)
    {
        _hashService = hashService;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task Register(CreateUserDto createUserDto)
    {
        var createUser = _mapper.Map<User>(createUserDto);
        createUser.Password = _hashService.HashPassword(createUser.Password);
        createUser.RoleId = 1;
        await _userRepository.InsertAsync(createUser);
        await _userRepository.SaveChangesAsync();
    }

    public async Task Login(LoginDto loginDto)
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

    }
}