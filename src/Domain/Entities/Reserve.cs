using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Base;

namespace Domain.Entities;

public class Reserve:BaseEntity
{
    public DateTime ReserveDate { get; set; } = DateTime.Now;

    public int PetId { get; set; }
    [ForeignKey("PetId")]
    public Pet Pet { get; set; } = null!;

    public int UserId { get; set; }
    [ForeignKey("UserId")] 
    public User User { get; set; } = null!;
}