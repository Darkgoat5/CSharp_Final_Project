using MovieTracker.WebAPI.Repository;
using Microsoft.EntityFrameworkCore;
using MovieTracker.WebAPI.DB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// Use scoped for repositories that depend on a DbContext (DbContext is scoped)
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddDbContext<MovieDBContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
