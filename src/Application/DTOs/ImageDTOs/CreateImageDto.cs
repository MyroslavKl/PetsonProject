using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.ImageDTOs;

public class CreateImageDto
{
    [Required, Url] 
    public string Url { get; set; } = string.Empty;
}