using Application.ActionFilters.RoleFilters;
using Application.ActionFilters.UserFilters;
using Application.DTOs.UserDTOs;
using Application.Persistence.Repositories;
using Application.Persistence.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        public UserController(IUserService userService,IUserRepository userRepository,IRoleRepository roleRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        [HttpGet]
        public IEnumerable<UserDto> GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return users;
        }

        [HttpGet("email")]
        [TypeFilter(typeof(UserExistByEmailFilterAttribute))]
        public async Task<UserDto> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            return user;
        }

        [HttpGet("{userId}")]
        [TypeFilter(typeof(UserExistByIdFilterAttribute))]
        public async Task<UserDto> GetUserById([FromRoute] int userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            return user;
        }

        [HttpPatch("name/{userId}")]
        [TypeFilter(typeof(UserExistByIdFilterAttribute))]
        public async Task ChangeName(string firstName,string lastName,[FromRoute]int userId)
        {
            var user = await _userRepository.GetOneAsync(obj => obj.Id == userId);
            await _userService.UpdateUserFullNameAsync(firstName,lastName, user);
        }
        [HttpPatch("password/{userId}")]
        [TypeFilter(typeof(UserExistByIdFilterAttribute))]
        public async Task ChangePassword(string password,[FromRoute]int userId)
        {
            var user = await _userRepository.GetOneAsync(obj => obj.Id == userId);
            await _userService.UpdatePasswordAsync(password, user);
        }

        [HttpPatch("{userId}/grand/{roleId}")]
        [TypeFilter(typeof(RoleExistByIdFilterAttribute))]
        [TypeFilter(typeof(UserExistByIdFilterAttribute))]
        public async Task RoleGrant([FromRoute]int userId,[FromRoute]int roleId)
        {
            var user = await _userRepository.GetOneAsync(obj => obj.Id == userId);
            var role = await _roleRepository.GetOneAsync(obj => obj.Id == roleId);
            await _userService.GrandRoleAsync(user,role);
        }
         
        [HttpDelete("{userId}")]
        [TypeFilter(typeof(UserExistByIdFilterAttribute))]
        public async Task<IActionResult> DeleteAccount([FromRoute] int userId)
        {
            var user = await _userRepository.GetOneAsync(obj => obj.Id == userId);
            await _userService.DeleteAccountAsync(user);
            return Ok("Account successfully deleted");
        }


    }
}
