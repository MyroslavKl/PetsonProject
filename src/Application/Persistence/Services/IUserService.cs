using Application.DTOs.UserDTOs;
using Domain.Entities;

namespace Application.Persistence.Services;

public interface IUserService
{
    IEnumerable<UserDto> GetAllUsers();
    Task<UserDto> GetUserByEmail(string email);
    Task<UserDto> GetUserById(int id);
    Task UpdateUserName(string userName,UpdateUserDto user);
    Task UpdatePassword(string password,UpdateUserDto user);
    Task GrandRole(User user,Role role);
    Task DeleteAccount(User user);
}