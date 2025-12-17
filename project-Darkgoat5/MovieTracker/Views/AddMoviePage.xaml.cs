using MovieTracker.Models;
using MovieTracker.ViewModels;
using MovieTracker.MovieService;

namespace MovieTracker;

public partial class AddMoviePage : ContentPage
{
    AddMoviePageViewModel vm;

    public AddMoviePage(AddMoviePageViewModel viewModel)
    {
        InitializeComponent();
        vm = viewModel;
        BindingContext = vm;
    }

    public AddMoviePage(IMovieService movieService, Movie existing)
    {
        InitializeComponent();
        vm = new AddMoviePageViewModel(movieService, existing);
        BindingContext = vm;
        // sets up a listener/delegate for when RequestClose.Invoke is called in the viewmodel
        vm.RequestClose = () => Navigation.PopModalAsync();
    }
}