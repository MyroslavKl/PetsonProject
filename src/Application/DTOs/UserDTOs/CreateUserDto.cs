using System.ComponentModel.DataAnnotations;
using Application.DTOs.AuthDtos;

namespace Application.DTOs.UserDTOs;

public class CreateUserDto:LoginDto
{
    [Required,MaxLength(25)]
    public string FirstName { get; set; } = string.Empty;
    [Required,MaxLength(25)]
    public string LastName { get; set; } = string.Empty;
    [Required]
    public string Gender { get; set; } = string.Empty;
}