using MovieTracker.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using MovieTracker.WebAPI.DB;

namespace MovieTracker.WebAPI.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieDBContext _db;
        public MovieRepository(MovieDBContext db) => _db = db;

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await _db.Movies.ToListAsync();
        }

        // only for testing
        public async Task<Movie?> GetMovieByIdAsync(int id)
        {
            return await _db.Movies.FindAsync(id);
        }

        public async Task AddMovieAsync(Movie movie)
        {
            // stages movie to be added
            await _db.Movies.AddAsync(movie);
            // actually adds movie
            await _db.SaveChangesAsync();
        }

        public async Task UpdateMovieAsync(int id, Movie updatedMovie)
        {
            var movie = await _db.Movies.FindAsync(id);
            if (movie != null)
            {
                movie.Title = updatedMovie.Title;
                movie.Genre = updatedMovie.Genre;
                movie.Year = updatedMovie.Year;
                movie.Rating = updatedMovie.Rating;
                // actually saves the changes to DB
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteMovieAsync(int id)
        {
            var movie = await _db.Movies.FindAsync(id);
            if (movie != null)
            {
                // marks movie for deletion
                _db.Movies.Remove(movie);
                // movies only gets deleted when savechanes is called
                await _db.SaveChangesAsync();
            }
        }
    }
}
