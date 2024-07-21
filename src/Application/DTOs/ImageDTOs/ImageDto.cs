namespace Application.DTOs.ImageDTOs;

public class ImageDto
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public int PetId { get; set; }
}