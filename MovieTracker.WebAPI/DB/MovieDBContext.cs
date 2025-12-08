using Microsoft.EntityFrameworkCore;
using MovieTracker.WebAPI.Models;

namespace MovieTracker.WebAPI.DB
{
    public class MovieDBContext: DbContext
    {
        public MovieDBContext(DbContextOptions<MovieDBContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; } = null!;
    }
}
