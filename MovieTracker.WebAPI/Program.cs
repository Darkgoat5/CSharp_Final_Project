using MovieTracker.WebAPI.Repository;
using Microsoft.EntityFrameworkCore;
using MovieTracker.WebAPI.DB;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IMovieRepository, MovieRepository>();

// configure DBContext to use MySQL with connection string from appsettings.json, gotten from slides about web API DIA 29
// using a fixed version of MYSQL to prevent api from sometimes failing because it looks for a version before MYSQL started
builder.Services.AddDbContext<MovieDBContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 21))));

var app = builder.Build();

// applies the DBContext migration at startup
// https://www.youtube.com/watch?v=VE2deocbXM0
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MovieDBContext>();
    dbContext.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
