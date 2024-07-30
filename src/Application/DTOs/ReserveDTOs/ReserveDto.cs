namespace Application.DTOs.ReserveDTOs;

public class ReserveDto
{
    public int Id { get; set; }
    public DateTime ReserveDate { get; set; } = DateTime.Now;
    public int PetId { get; set; }
    public int UserId { get; set; }
}