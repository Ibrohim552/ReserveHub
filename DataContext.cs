using Microsoft.EntityFrameworkCore;
using ReserveHub.Entities;

namespace ReserveHub;

public class DataContext:DbContext
{
    public DbSet<Booking> Booking { get;  set; }
    public DbSet<Client> Client { get; set; }
    public DbSet<BusinessOwner> BusinessOwner { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }
}