namespace ReserveHub.Filters;

public record  ClientFilter:BaseFilter
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public Gender? Gender { get; set; }
}