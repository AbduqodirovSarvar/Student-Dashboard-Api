using Microsoft.EntityFrameworkCore;
using Student_Dashboard_Api.Data;
using Student_Dashboard_Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.ApiServices(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<AppDbContext>();
try
{
    context.Database.Migrate();
    Console.WriteLine("Migrations applying successfully completed");
}
catch (Exception ex)
{
    Console.WriteLine($"Error applying migrations: {ex.Message}");
}

app.MapControllers();

app.Run();
