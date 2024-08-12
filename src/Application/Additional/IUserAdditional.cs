using Domain.Entities;

namespace Application.Additional;

public interface IUserAdditional
{
    Task UpdateMethodAsync(string firstName,string lastName, User user);
}