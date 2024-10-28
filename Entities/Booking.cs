namespace ReserveHub.Entities;

public class Booking :BaseEntity
{
    public int ClientId { get; set; }
    public Client Client { get; set; }
    public int BusinessOwnerId { get; set; }
    public BusinessOwner BusinessOwner { get; set; }
    public DateTime BookingDate { get; set; }
    public decimal Price { get; set; }
    
}