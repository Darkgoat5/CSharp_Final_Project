using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MovieTracker.Models;
using MovieTracker.MovieService;
using Microsoft.Maui.Controls;


namespace MovieTracker.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly IMovieService _movieService;
        public ObservableCollection<Movie> MovieList => _movieService.Movies;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Command DeleteMovieCommand { get; }
        public Command EditMovieCommand { get; }

        private Movie? _selectedMovie;
        public Movie? SelectedMovie
        {
            get => _selectedMovie;
            set
            {
                if (_selectedMovie == value)
                {
                    return;
                }

                _selectedMovie = value;
                DeleteMovieCommand.ChangeCanExecute();
                EditMovieCommand.ChangeCanExecute();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedMovie)));
            }
        }

        public MainPageViewModel(IMovieService movieService)
        {
            _movieService = movieService;
            DeleteMovieCommand = new Command(async () => await DeleteSelectedMovie(), () => SelectedMovie != null);
            EditMovieCommand = new Command(EditSelectedMovie, () => SelectedMovie != null);
            
            // start loading movies on creation of viewmodel
            _ = LoadMoviesAsync();
        }

        private async Task LoadMoviesAsync()
        {
            await _movieService.LoadMoviesAsync();
        }

        private async Task DeleteSelectedMovie()
        {
            // selectedmovie can never be NULL because of canexecute on command
            await _movieService.DeleteMovieAsync(SelectedMovie);
            SelectedMovie = null;
        }

        private void EditSelectedMovie()
        {
            // get the current first page of the app
            var page = Application.Current?.Windows[0]?.Page;
            if (page != null)
            {
                // same as with delete, can never be null because of canexecute
                // pushes the add movie page as a modal with the selected movie for editing
                page.Navigation.PushModalAsync(new AddMoviePage(_movieService, SelectedMovie));
            }
        }
    }
}
