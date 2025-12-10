using MovieTracker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace MovieTracker.MovieService
{
    public interface IMovieService
    {
        ObservableCollection<Movie> Movies { get; }
        Task LoadMoviesAsync();
        Task AddMovieAsync(Movie movie);
        Task DeleteMovieAsync(Movie movie);
        Task EditMovieAsync(Movie movie);
        int GetNextId();
    }
}
