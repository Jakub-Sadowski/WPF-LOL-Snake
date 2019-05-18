using System.Media;
using System.Windows;

namespace WPFsnejk
{
    /// <summary>
    /// Interaction logic for pauseWindow.xaml
    /// </summary>
    public partial class pauseWindow : Window
    {
        public pauseWindow()
        {
           
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
           
        }

        private void resume_Game(object sender, RoutedEventArgs e)
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.Name == "gameWindow")
                {
                    window.Visibility = Visibility.Visible;

                }
            }
            Visibility = Visibility.Collapsed;
        }

        private void restart_Game(object sender, RoutedEventArgs e)
        {
            foreach(Window window in App.Current.Windows)
            {
                if (window.Name == "gameWindow")
                    window.Close();
            }
            Application.Current.MainWindow.Visibility = Visibility.Visible;
            Close();
        }

        private void quit_Game(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}