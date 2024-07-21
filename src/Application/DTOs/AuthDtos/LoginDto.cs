using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.AuthDtos;

public class LoginDto
{
    [Required, EmailAddress]
    public string Email { get; init; }
    
    [Required, MinLength(8), MaxLength(100)]
    public string Password { get; init; }
}