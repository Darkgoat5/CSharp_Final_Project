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
        void AddMovie(Movie movie);
        void DeleteMovie(Movie movie);
        void EditMovie(Movie movie);
        int GetNextId();
    }
}
