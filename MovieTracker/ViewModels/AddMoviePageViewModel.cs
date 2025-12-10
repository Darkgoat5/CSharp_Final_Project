using Microsoft.Maui.Controls;
using MovieTracker.Models;
using MovieTracker.MovieService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MovieTracker.ViewModels
{
    public class AddMoviePageViewModel : INotifyPropertyChanged
    {
        private readonly IMovieService _movieService;

        public event PropertyChangedEventHandler? PropertyChanged;

        public List<int> Ratings { get; } = new List<int> { 1, 2, 3, 4, 5 };

        public Command AddMovieCommand { get; }
        public Command? CancelCommand { get; }

        public string ButtonText { get; set; }

        public bool IsEditMode;
        public bool Cancelable { get; set; }

        // paramerless delegate to request closing the page
        public Func<Task>? RequestClose;

        public int _Id { get; set; }

        public AddMoviePageViewModel(IMovieService movieService)
        {
            _movieService = movieService;
            _selectedDate = DateTime.Today; // set date of datepicker to today
            AddMovieCommand = new Command(async () => await AddMovie(), CanAddMovie);
            ButtonText = "Add Movie";
            IsEditMode = false;
            Cancelable = false;
        }

        public AddMoviePageViewModel(IMovieService movieService, Movie ExistingMovie)
        {
            _movieService = movieService;
            AddMovieCommand = new Command(async () => await AddMovie(), CanAddMovie);
            CancelCommand = new Command(() => _ = RequestClose?.Invoke());
            ButtonText = "Save Edited Movie";
            _Id = ExistingMovie.Id;
            NewTitle = ExistingMovie.Title;
            NewGenre = ExistingMovie.Genre;
            SelectedDate = ExistingMovie.Year.ToDateTime(new TimeOnly(0, 0));
            SelectedRating = ExistingMovie.Rating.Length;
            IsEditMode = true;
            Cancelable = true;
        }

        private async Task AddMovie()
        {
            var m = new Movie
            {
                Title = NewTitle,
                Genre = NewGenre,
                Year = DateOnly.FromDateTime(SelectedDate), // datepicker gives Datetime, so convert to DateOnly
                Rating = new string('★', SelectedRating) // puts X amount of stars in Rating string
            };

            if (IsEditMode)
            {
                m.Id = _Id;
                await _movieService.EditMovieAsync(m); // uses the async edit function from movie service
                _ = RequestClose?.Invoke(); // _ -> fire and forget, prevents the delegate from leaving the Task unobserved
            }
            else 
            {
                m.Id = _movieService.GetNextId();
                await _movieService.AddMovieAsync(m); // use async Add function from movie service
            }

            NewTitle = string.Empty;
            NewGenre = string.Empty;
            SelectedDate = DateTime.Today;
            SelectedRating = 0;
            IsEditMode = false;
        }

        private string _newTitle = string.Empty;
        public string NewTitle
        {
            get => _newTitle;
            set
            {
                if (SetProperty(ref _newTitle, value))
                {
                    AddMovieCommand.ChangeCanExecute();
                }
            }
        }

        private string _newGenre = string.Empty;
        public string NewGenre
        {
            get => _newGenre;
            set
            {
                if (SetProperty(ref _newGenre, value))
                {
                    AddMovieCommand.ChangeCanExecute();
                }
            }
        }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                if (SetProperty(ref _selectedDate, value))
                {
                    // notifies that IsDateInvalid may have changed
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsDateInvalid)));
                    AddMovieCommand.ChangeCanExecute();
                }
            }
        }

        private int _selectedRating;
        public int SelectedRating
        {
            get => _selectedRating;
            set
            {
                if (SetProperty(ref _selectedRating, value))
                {
                    AddMovieCommand.ChangeCanExecute();
                }
            }
        }

        // seperate property because its used to bind to the UI to change IsVisible
        public bool IsDateInvalid => SelectedDate.Date > DateTime.Today;
        private bool CanAddMovie()
        {
            return !string.IsNullOrWhiteSpace(NewTitle)
                && !string.IsNullOrWhiteSpace(NewGenre)
                && !IsDateInvalid
                && SelectedRating > 0;
        }

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string? propertyName = null)
        {
            // checks if new value is the same as old value
            if (EqualityComparer<T>.Default.Equals(backingStore, value)) return false;
            backingStore = value;
            // notify that property changed
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            return true;
        }
    }
}
