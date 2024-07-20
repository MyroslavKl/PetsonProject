using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Base;

namespace Domain.Entities;

public class Image:BaseEntity
{
    public string Url { get; set; } = string.Empty;
        
    public int PetId { get; set; }
    [ForeignKey("PetId")]
    public Pet Pet { get; set; } = null!;
}