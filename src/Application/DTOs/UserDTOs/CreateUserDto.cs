using System.ComponentModel.DataAnnotations;
using Application.DTOs.AuthDtos;

namespace Application.DTOs.UserDTOs;

public class CreateUserDto:LoginDto
{
    [MaxLength(25)]
    public string FirstName { get; set; } = string.Empty;
    [MaxLength(25)] 
    public string LastName { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string Gender { get; set; } = string.Empty;
}