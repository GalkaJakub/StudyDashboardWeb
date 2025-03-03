using Microsoft.EntityFrameworkCore;
using Study_dashboard_API.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StudyDsManagement"));
});

// Add services to the container.
builder.Services.AddControllers();
var app = builder.Build();


app.UseHttpsRedirection();


app.MapControllers();

app.Run();

