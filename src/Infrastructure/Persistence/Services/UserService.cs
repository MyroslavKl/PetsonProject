using Application.Additional;
using Application.DTOs.UserDTOs;
using Application.Persistence.Repositories;
using Application.Persistence.Services;
using AutoMapper;
using CacheServices.Service;
using Domain.Entities;

namespace Infrastructure.Persistence.Services;

public class UserService:IUserService
{

    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;
    private readonly IUserAdditional _userAdditional;

    public UserService(IUserRepository userRepository,IMapper mapper,ICacheService cacheService,IUserAdditional userAdditional)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _cacheService = cacheService;
        _userAdditional = userAdditional;
    }
    
    public IEnumerable<UserDto> GetAllUsers()
    {
        var cacheDataUsers = _cacheService.GetData<IEnumerable<UserDto>>("user");
        if (cacheDataUsers != null && cacheDataUsers.Count() > 0)
        {
            return cacheDataUsers;
        }

        var users = _userRepository.GetAll();
        cacheDataUsers = _mapper.Map<IEnumerable<UserDto>>(users);

        var expirationTime = DateTime.Now.AddMinutes(3);
        _cacheService.SetData("user",cacheDataUsers,expirationTime);
        
        return cacheDataUsers;
    }

    public async Task<UserDto> GetUserByEmailAsync(string email)
    {
        var userByEmail = await _userRepository.GetOneAsync(user => user.Email == email);
        var userByEmailDto = _mapper.Map<UserDto>(userByEmail);
        return userByEmailDto;
    }

    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        var userCache = _cacheService.GetData<UserDto>($"user{id}");
        if (userCache != null)
        {
            return userCache;
        }
        var userById = await _userRepository.GetOneAsync(user => user.Id == id);
        var userByIdDto = _mapper.Map<UserDto>(userById);
        return userByIdDto;
    }

    public async Task UpdateUserFullNameAsync(string firstName,string lastName, User user)
    {
        var userCache = _cacheService.GetData<UserDto>($"user{user.Id}");
        var userDto = _mapper.Map<UserDto>(user);
        var expirationTime = DateTime.Now.AddMinutes(3);
        await _userAdditional.UpdateMethodAsync(firstName,lastName,user);
        if (userCache != null)
        {
           _cacheService.RemoveData($"user{user.Id}");
           _cacheService.SetData($"user{user.Id}",userDto,expirationTime);
        }
        else
        {
            _cacheService.SetData($"user{user.Id}",userDto,expirationTime);
        }

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
        
        _cacheService.RemoveData($"user{user.Id}");
    }
}