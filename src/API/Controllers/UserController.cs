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
        public async Task<UserDto> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmail(email);
            return user;
        }

        [HttpGet("{userId}")]
        public async Task<UserDto> GetUserById([FromRoute] int userId)
        {
            var user = await _userService.GetUserById(userId);
            return user;
        }

        [HttpPatch("name/{userId}")]
        public async Task ChangeName(string firstName,string lastName,[FromRoute]int userId)
        {
            var user = await _userRepository.GetOneAsync(obj => obj.Id == userId);
            await _userService.UpdateUserFullName(firstName,lastName, user);
        }
        [HttpPatch("password/{userId}")]
        public async Task ChangePassword(string password,[FromRoute]int userId)
        {
            var user = await _userRepository.GetOneAsync(obj => obj.Id == userId);
            await _userService.UpdatePassword(password, user);
        }

        [HttpPatch("{userId}/grand/{roleId}")]
        public async Task RoleGrant([FromRoute]int userId,[FromRoute]int roleId)
        {
            var user = await _userRepository.GetOneAsync(obj => obj.Id == userId);
            var role = await _roleRepository.GetOneAsync(obj => obj.Id == roleId);

            await _userService.GrandRole(user,role);
        }
         
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteAccount([FromRoute] int userId)
        {
            var user = await _userRepository.GetOneAsync(obj => obj.Id == userId);
            await _userService.DeleteAccount(user);
            return Ok("Account successfully deleted");
        }


    }
}
