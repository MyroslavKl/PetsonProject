namespace Application.Persistence.Services.HashServices;

public interface IHashService
{
    public string HashPassword(string password);
    
    public bool VerifyPassword(string hashPassword, string rawPassword);
}