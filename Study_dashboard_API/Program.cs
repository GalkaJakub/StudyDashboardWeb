using Microsoft.EntityFrameworkCore;
using Study_dashboard_API.Data;
using Study_dashboard_API.Filters.OperationsFilters;
using Microsoft.OpenApi.Models;
using Study_dashboard_API.Security;

var builder = WebApplication.CreateBuilder(args);

// Configure Entity Framework with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StudyDsManagement"));
});

// Add services to the container.
builder.Services.AddControllers();

// Register Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<AuthorizationHeaderOperationFilter>();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });
});

// Register password hasher service
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

var app = builder.Build();

// Enable Swagger only in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enforce HTTPS
app.UseHttpsRedirection();

// Map controller routes
app.MapControllers();

// Run the application
app.Run();

