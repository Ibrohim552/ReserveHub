namespace ReserveHub.Entities;

public class Client:BaseEntity
{ 
    public string?  Name { get; set; }
    public string? Surname { get; set; }
    public IEnumerable<Booking> Bookings { get; set; }
}