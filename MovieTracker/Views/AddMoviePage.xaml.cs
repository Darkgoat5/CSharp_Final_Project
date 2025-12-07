using MovieTracker.Models;
using MovieTracker.ViewModels;


namespace MovieTracker;

public partial class AddMoviePage : ContentPage
{
    AddMoviePageViewModel vm;

    public AddMoviePage(Movie existing = null)
    {
        InitializeComponent();
        // if no existing movie is passed, we add a new one, otherwise we edit the existing one
        vm = existing == null ? new AddMoviePageViewModel() : new AddMoviePageViewModel(existing);
        BindingContext = vm;
        // sets up a listener/delegate for when RequestClose.Invoke is called in the viewmodel
        vm.RequestClose = () => Navigation.PopModalAsync();
    }
}