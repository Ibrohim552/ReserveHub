namespace ReserveHub.Filters;


public record BookingFilter : BaseFilter
{
    public string ClientName { get; set; }
    public string ClientSurname { get; set; }
    public string ClientEmail { get; set; }
    public string ClientPhoneNumber { get; set; }
    public DateTime? BookingDate { get; set; }
    public string Status { get; set; }
    public decimal Price { get; set; }
}