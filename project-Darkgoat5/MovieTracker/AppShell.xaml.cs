namespace MovieTracker
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(AddMoviePage), typeof(AddMoviePage));
            InitializeComponent();
        }
    }
}
