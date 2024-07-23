using Application.DTOs.UserDTOs;
using Application.Persistence.Repositories;
using Application.Persistence.Services;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Persistence.Services;

public class UserService:IUserService
{

    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository,IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public IEnumerable<UserDto> GetAllUsers()
    {
        var users = _userRepository.GetAllAsync();
        var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);
        return usersDto;
    }

    public async Task<UserDto> GetUserByEmail(string email)
    {
        var userByEmail = await _userRepository.GetOneAsync(user => user.Email == email);
        var userByEmailDto = _mapper.Map<UserDto>(userByEmail);
        return userByEmailDto;
    }

    public async Task<UserDto> GetUserById(int id)
    {
        var userById = await _userRepository.GetOneAsync(user => user.Id == id);
        var userByIdDto = _mapper.Map<UserDto>(userById);
        return userByIdDto;
    }

    public async Task UpdateUserName(string userName, UpdateUserDto userDto) /////FullName need
    {
        userDto.FirstName = userName;
        var user = _mapper.Map<User>(userDto);
        _userRepository.UpdateAsync(user);
        await _userRepository.SaveChangesAsync();
    }

    public async Task UpdatePassword(string password, UpdateUserDto userDto)
    {
        userDto.Password = password;
        var user = _mapper.Map<User>(userDto);
        _userRepository.UpdateAsync(user);
        await _userRepository.SaveChangesAsync();
    }

    public async Task GrandRole(User user, Role role)
    {
        user.RoleId = role.Id;
        _userRepository.UpdateAsync(user);
        await _userRepository.SaveChangesAsync();
    }

    public async Task DeleteAccount(User user)
    {
        _userRepository.DeleteAsync(user);
        await _userRepository.SaveChangesAsync();
    }
}