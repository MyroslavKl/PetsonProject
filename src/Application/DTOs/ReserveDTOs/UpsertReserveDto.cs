namespace Application.DTOs.ReserveDTOs;

public class UpsertReserveDto
{
    public DateTime ReserveDate { get; set; } = DateTime.Now;
    public int PetId { get; set; }
    public int UserId { get; set; }
}