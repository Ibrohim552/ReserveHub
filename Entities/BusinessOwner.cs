namespace ReserveHub.Entities;

public class BusinessOwner:BaseEntity
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Address { get; set; }
    public int Expiriens { get; set; }
    public string? BisnessType { get; set; }
    public IEnumerable<Booking> Bookings { get; set; }
}