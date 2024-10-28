using Microsoft.EntityFrameworkCore;
using ReserveHub;
using ReserveHub.Filters;
using ReserveHub.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IClientService, ClientService>();
builder.Services.AddTransient<IBusinessOwnerService, BusinessOwnerService>();
builder.Services.AddTransient<IBookingService, BookingService>();
builder.Services.AddAutoMapper(typeof(Mapper));
builder.Services.AddControllers();

var app = builder.Build();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.MapControllers();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.Run();


