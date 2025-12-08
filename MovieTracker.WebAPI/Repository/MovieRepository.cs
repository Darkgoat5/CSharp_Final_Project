using MovieTracker.WebAPI.Models;

namespace MovieTracker.WebAPI.Repository
{
    public class MovieRepository: IMovieRepository
    {
        List<Movie> MovieList = new List<Movie>
        {
            new Movie { Id = 1, Title = "Movie1", Genre = "Genre1", Year = new DateOnly(1990, 2, 25), Rating = "★★★" },
            new Movie { Id = 2, Title = "Movie2", Genre = "Genre2", Year = new DateOnly(2000, 5, 15), Rating = "★★★★★" },
            new Movie { Id = 3, Title = "Movie3", Genre = "Genre3", Year = new DateOnly(2010, 8, 10), Rating = "★★" },
            new Movie { Id = 4, Title = "Movie4", Genre = "Genre4", Year = new DateOnly(2020, 12, 5), Rating = "★" }
        };

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await Task.FromResult(MovieList);
        }

        public async Task<Movie?> GetMovieByIdAsync(int id)
        {
            var movie = MovieList.FirstOrDefault(m => m.Id == id);
            return await Task.FromResult(movie);
        }

        public async Task AddMovieAsync(Movie movie)
        {
            var movieList = MovieList.ToList();
            movieList.Add(movie);
            MovieList = movieList;
            await Task.CompletedTask;
        }

        public async Task UpdateMovieAsync(int id, Movie updatedMovie)
        {
            var movieList = MovieList.ToList();
            var index = movieList.FindIndex(m => m.Id == id);
            if (index != -1)
            {
                movieList[index] = updatedMovie;
                MovieList = movieList;
            }
            await Task.CompletedTask;
        }

        public async Task DeleteMovieAsync(int id)
        {
            var movieList = MovieList.ToList();
            var movieToRemove = movieList.FirstOrDefault(m => m.Id == id);
            if (movieToRemove != null)
            {
                movieList.Remove(movieToRemove);
                MovieList = movieList;
            }
            await Task.CompletedTask;
        }
    }
}
