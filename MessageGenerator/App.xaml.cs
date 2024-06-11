using MessageGenerator.Controller;
using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MessageGenerator
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            var dummyLoad = new DummyLoadControl();
            RoutedEventHandler initAct = (object sender, RoutedEventArgs e) =>
            {
                _controller.AfterLoadedUIInit();
            };
            dummyLoad.Loaded += initAct;
            var mainWindow = new MainWindow();
            mainWindow.LoadedWindow += initAct;
            _controller = new ReservationController(mainWindow);
            m_window = mainWindow;
            m_window.Activate();
        }

        private Window m_window;
        private ReservationController _controller;
    }
}
