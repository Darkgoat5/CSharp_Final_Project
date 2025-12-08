using MovieTracker.WebAPI.Models;

namespace MovieTracker.WebAPI.Repository
{
    public interface IMovieRepository
    {
		Task<IEnumerable<Movie>> GetAllMoviesAsync();
		Task<Movie?> GetMovieByIdAsync(int id);
		Task AddMovieAsync(Movie movie);
		Task UpdateMovieAsync(int id, Movie updatedMovie);
		Task DeleteMovieAsync(int id);
    }
}
