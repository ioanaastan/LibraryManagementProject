using Microsoft.EntityFrameworkCore;
using LibraryManagement.Core;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Database;
using LibraryManagement.Database.Repositories;
using LibraryManagement.Infrastructure.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;
using LibraryManagement.Api.Middleware;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Library Management API",
        Version = "v1",
        Description = "API for retrieving books and their borrowing records"
    });

    // Set up XML comments - with error handling
    try
    {
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
        if (File.Exists(xmlPath))
        {
            options.IncludeXmlComments(xmlPath);
        }
    }
    catch (Exception)
    {
        // Silently continue if XML file not found
    }
});

// Register ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// Register repositories
builder.Services.AddScoped<IBookRepository, BookRepository>();

// Register services
builder.Services.AddScoped<IBookService, BookService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandling();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Seed data to the database
try
{
    await DataSeeder.SeedDataAsync(app.Services);
}
catch (Exception ex)
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred while seeding the database.");
}

app.Run();