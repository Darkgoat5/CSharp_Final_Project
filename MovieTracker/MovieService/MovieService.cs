using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using MovieTracker.Models;

namespace MovieTracker.MovieService
{
    public class MovieService: IMovieService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost:5000/api/MovieTrackerAPI/";

        public ObservableCollection<Movie> Movies { get; } = new ObservableCollection<Movie>();

        public MovieService(HttpClient httpClient) 
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(BaseUrl);
        }

        public async Task LoadMoviesAsync()
        {
            try
            {
                // send http request with just base URl
                var movies = await _httpClient.GetFromJsonAsync<List<Movie>>("");
                Movies.Clear();
                if (movies != null)
                {
                    foreach (var movie in movies)
                    {
                        Movies.Add(movie);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"error: {e.Message}");
            }
        }

        public async Task AddMovieAsync(Movie movie)
        {
            try
            {
                // send http post request to base URL with movie as json body
                var response = await _httpClient.PostAsJsonAsync("", movie);
                if (response.IsSuccessStatusCode)
                {
                    Movies.Add(movie);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"error: {e.Message}");
            }
        }

        public async Task DeleteMovieAsync(Movie movie)
        {
            try
            {
                // send http delete request to base URL with movie id
                var response = await _httpClient.DeleteAsync($"{movie.Id}");
                if (response.IsSuccessStatusCode)
                {
                    Movies.Remove(movie);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"error: {e.Message}");
            }
        }

        public async Task EditMovieAsync(Movie movie)
        {
            try
            {
                // send http put request to base URL with movie id and edited movie as json body
                var response = await _httpClient.PutAsJsonAsync($"{movie.Id}", movie);
                if (response.IsSuccessStatusCode)
                {
                    await LoadMoviesAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"error: {e.Message}");
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
