using Application.DTOs.UserDTOs;
using Domain.Entities;

namespace Application.Persistence.Services;

public interface IUserService
{
    IEnumerable<UserDto> GetAllUsers();
    Task<UserDto> GetUserByEmail(string email);
    Task<UserDto> GetUserById(int id);
    Task UpdateUserFullName(string firstName,string lastName,User user);
    Task UpdatePassword(string password,User user);
    Task GrandRole(User user,Role role);
    Task DeleteAccount(User user);
}