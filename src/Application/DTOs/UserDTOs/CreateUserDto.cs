using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.UserDTOs;

public class CreateUserDto
{
    [Required,MaxLength(25)]
    public string FirstName { get; set; } = string.Empty;
    [Required,MaxLength(25)]
    public string LastName { get; set; } = string.Empty;
}