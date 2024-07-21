using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.ImageDTOs;

public class UpdateImageDto
{
    [Required, Url] 
    public string Url { get; set; } = string.Empty;
}