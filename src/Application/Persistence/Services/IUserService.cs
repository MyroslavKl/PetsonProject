using Application.DTOs.UserDTOs;
using Domain.Entities;

namespace Application.Persistence.Services;

public interface IUserService
{
    IEnumerable<UserDto> GetAllUsers();
    Task<UserDto> GetUserByEmailAsync(string email);
    Task<UserDto> GetUserByIdAsync(int id);
    Task UpdateUserFullNameAsync(string firstName,string lastName,User user);
    Task UpdatePasswordAsync(string password,User user);
    Task GrandRoleAsync(User user,Role role);
    Task DeleteAccountAsync(User user);
}