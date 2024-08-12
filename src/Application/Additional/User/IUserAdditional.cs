

namespace Application.Additional.User;

public interface IUserAdditional
{
    Task UpdateMethodAsync(string firstName,string lastName, Domain.Entities.User user);
}