namespace MovieTracker
{
    public partial class App : Application
    {
        private readonly AppShell Shell;

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MovieService.MovieService>();
            Shell = new AppShell(); // the app's root UI, defines tabs, routes and navigation
            MainPage = Shell; // makes the AppShell the main page

        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(Shell);
        }
    }
}