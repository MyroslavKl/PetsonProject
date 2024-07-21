namespace Application.DTOs.PetDTOs;

public class PetDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public DateTime StartDate { get; set; } //start date in shelter
    public DateTime LastDate { get; set; } //last date in shelter
    public string TypeOfPet { get; set; } = string.Empty;
    public string Species { get; set; } = string.Empty;
    public bool IsVaccination { get; set; } = true;
    public string Description { get; set; } = string.Empty;
}