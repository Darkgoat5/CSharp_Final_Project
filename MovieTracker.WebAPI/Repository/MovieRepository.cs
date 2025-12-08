using MovieTracker.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using MovieTracker.WebAPI.DB;

namespace MovieTracker.WebAPI.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieDBContext _db;
        public MovieRepository(MovieDBContext db) => _db = db;

        public Task<IEnumerable<Movie>> GetAllMoviesAsync() =>
            _db.Movies.ToListAsync().ContinueWith(t => (IEnumerable<Movie>)t.Result);

        public async Task<Movie?> GetMovieByIdAsync(int id)
        {
            var movie = await _db.Movies.FindAsync(id);
            return movie;
        }

        public async Task AddMovieAsync(Movie movie)
        {
            await _db.Movies.AddAsync(movie);
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
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteMovieAsync(int id)
        {
            var movie = await _db.Movies.FindAsync(id);
            if (movie != null)
            {
                _db.Movies.Remove(movie);
                await _db.SaveChangesAsync();
            }
        }
    }
}
