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
        public IMovieService MovieService => DependencyService.Get<IMovieService>();
        public ObservableCollection<Movie> MovieList => MovieService.Movies;

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

        public MainPageViewModel()
        {
            DeleteMovieCommand = new Command(DeleteSelectedMovie, () => SelectedMovie != null);
            EditMovieCommand = new Command(EditSelectedMovie, () => SelectedMovie != null);
        }

        private void DeleteSelectedMovie()
        {
            MovieList.Remove(SelectedMovie);
            SelectedMovie = null;
        }

        private void EditSelectedMovie()
        {
            Application.Current.MainPage.Navigation.PushModalAsync(new AddMoviePage(SelectedMovie));
        }
    }
}
