using System.Media;
using System.Windows;

namespace WPFsnejk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void startButton(object sender, RoutedEventArgs e)
        {
            string lvl = "Easy";
            if (ez.IsChecked.Value)
                lvl = "Easy";
            if (md.IsChecked.Value)
                lvl = "Medium";
            if (hd.IsChecked.Value)
                lvl = "Hard";

            Window1 x = new Window1(lvl);
            x.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Collapsed;
        }
        private void exitButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void music_CheckedChanged(object sender, RoutedEventArgs e)
        {


            SoundPlayer snd = new SoundPlayer("D:/pobrane/WPFsnejk/WPFsnejk/Resources/Music/theme.wav");
            
            if (Music.IsChecked == true)
            {
                snd.Play();
            }
            if (Music.IsChecked == false)
            {
                snd.Stop();
            }

        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}