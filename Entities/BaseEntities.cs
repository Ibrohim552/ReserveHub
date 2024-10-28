namespace ReserveHub.Entities;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string? PhoneNumber { get; set; }
    public bool  IsDeleted { get; set; }
    public DateTime DeletedAt { get; set; } = DateTime.UtcNow;
    public string? Email { get; set; }
    public Gender Gender { get; set; } = Gender.Male;
    public string? Status { get; set; }

}