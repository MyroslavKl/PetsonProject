using Application.Persistence.Repositories;
using Domain.Entities;

namespace Application.Additional;

public class UserAdditional:IUserAdditional
{
    private readonly IUserRepository _userRepository;

    public UserAdditional(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task UpdateMethodAsync(string firstName, string lastName, User user)
    {
        user.FirstName = firstName;
        user.LastName = lastName;
        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync();
    }
}