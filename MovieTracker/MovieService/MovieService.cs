using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MovieTracker.Models;

namespace MovieTracker.MovieService
{
    public class MovieService: IMovieService
    {
        public ObservableCollection<Movie> Movies { get; } = new ObservableCollection<Movie>();

        public MovieService() 
        { 
            
        }

        public void AddMovie(Movie movie)
        {
            Movies.Add(movie);
        }

        public void DeleteMovie(Movie movie)
        {
            Movies.Remove(movie);
        }

        public void EditMovie(Movie movie)
        {
            var existingMovie = Movies.FirstOrDefault(m => m.Id == movie.Id);
            if (existingMovie != null)
            {
                var index = Movies.IndexOf(existingMovie);
                Movies[index] = movie;
            }
        }

        public int GetNextId()
        {
            if (!Movies.Any())
                return 1;

            return Movies.Max(m => m.Id) + 1;
        }
    }
}
