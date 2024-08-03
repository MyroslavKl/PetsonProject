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
        var users = _userRepository.GetAll();
        var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);
        return usersDto;
    }

    public async Task<UserDto> GetUserByEmailAsync(string email)
    {
        var userByEmail = await _userRepository.GetOneAsync(user => user.Email == email);
        var userByEmailDto = _mapper.Map<UserDto>(userByEmail);
        return userByEmailDto;
    }

    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        var userById = await _userRepository.GetOneAsync(user => user.Id == id);
        var userByIdDto = _mapper.Map<UserDto>(userById);
        return userByIdDto;
    }

    public async Task UpdateUserFullNameAsync(string firstName,string lastName, User user) 
    {
        user.FirstName = firstName;
        user.LastName = lastName;
        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync();
    }

    public async Task UpdatePasswordAsync(string password, User user)
    {
        user.Password = password;
        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync();
    }

    public async Task GrandRoleAsync(User user, Role role)
    {
        user.RoleId = role.Id;
        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync();
    }

    public async Task DeleteAccountAsync(User user)
    {
        _userRepository.Delete(user);
        await _userRepository.SaveChangesAsync();
    }
}