using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.UserDTOs;

public class UpdateUserDto
{
    [Required,MaxLength(25)]
    public string FirstName { get; set; } = string.Empty;
    [Required,MaxLength(25)]
    public string LastName { get; set; } = string.Empty;
    [Required]
    public DateOnly DateOfBirth { get; set; }
    [Required,EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required, MinLength(8), MaxLength(100)]
    public string Password { get; set; } = string.Empty;
}