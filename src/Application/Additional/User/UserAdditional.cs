using Application.Persistence.Repositories;
using Domain.Entities;

namespace Application.Additional.User;

public class UserAdditional:IUserAdditional
{
    private readonly IUserRepository _userRepository;

    public UserAdditional(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task UpdateMethodAsync(string firstName, string lastName, Domain.Entities.User user)
    {
        user.FirstName = firstName;
        user.LastName = lastName;
        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync();
    }
}