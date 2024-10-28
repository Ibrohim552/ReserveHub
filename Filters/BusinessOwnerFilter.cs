namespace ReserveHub.Filters;


public record BusinessOwnerFilter : BaseFilter
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Address { get; set; }
    public string BusinessType { get; set; }
}
