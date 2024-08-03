using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.PetDTOs;

public class UpsertPetDto
{
    [Required,MaxLength(30)]
    public string Name { get; set; } = string.Empty;
    [Required]
    public DateOnly DateOfBirth { get; set; }
    [Required]
    public DateTime StartDate { get; set; } //start date in shelter
    public DateTime LastDate { get; set; } //last date in shelter
    [Required,MaxLength(25)]
    public string TypeOfPet { get; set; } = string.Empty;
    [Required,MaxLength(25)]
    public string Species { get; set; } = string.Empty;
    [Required]
    public bool IsVaccination { get; set; } = true;
    [Required,MaxLength(150)]
    public string Description { get; set; } = string.Empty;
}