using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace WPFsnejk
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        Random random = new Random();
        private DispatcherTimer timer;

        private int snakeSize = 15;



        private pauseWindow pW = new pauseWindow();

        private List<snakePart> apples = new List<snakePart>();
        private List<snakePart> rottens = new List<snakePart>();


        private snakePart newApple;
        private snakePart newRotten;

        private snake snejk;

        private string movement = "";
        private int score = 0;
        private bool vWalls;
        private string difficulty;
        private int applesEaten = 0;

        public Window1(string lvl)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            difficulty = lvl;
            walls();
            paintSnake();
            setTime();
            paintApple();
        }

        void walls()
        {
            for (int i = 0; i < gameGrid.Width / snakeSize; i++)
            {
                ColumnDefinition columnDefinition = new ColumnDefinition();
                columnDefinition.Width = new GridLength(snakeSize);
                gameGrid.ColumnDefinitions.Add(columnDefinition);
            }
            for (int j = 0; j < gameGrid.Height / snakeSize; j++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(snakeSize);
                gameGrid.RowDefinitions.Add(rowDefinition);
            }
            snejk = new snake();
            ImageBrush myBrush = new ImageBrush();
            
            myBrush.ImageSource =
                new BitmapImage(new Uri("D:/pobrane/WPFsnejk/WPFsnejk/Resources/Images/snakehead.png"));
            snejk.head.ellipse.Fill = myBrush;
        }

        private void noWalls()
        {
            if (snejk.head.X < 0)
                snejk.head.X = (int)(gameGrid.Width / snakeSize);
            else if (snejk.head.X > (gameGrid.Width / snakeSize))
                snejk.head.X = 0;
            else if (snejk.head.Y < 0)
                snejk.head.Y = (int)(gameGrid.Height / snakeSize);
            else if (snejk.head.Y > (gameGrid.Height / snakeSize))
                snejk.head.Y = 0;
            snejk.setOnGrid();
        }

        private void setTime()
        {
            int time = 800000;
            switch (difficulty)
            {
                case "Easy":
                    time = 800000;
                    break;
                case "Medium":
                    time = 500000;
                    break;
                case "Hard":
                    time = 200000;
                    break;
            }
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(tick);
            timer.Interval = new TimeSpan(time);
            timer.Start();
        }

        void tick(object sender, EventArgs e) => Logic();

        private void paintSnake()
        {
            gameGrid.Children.Add(snejk.head.ellipse);
            foreach (snakePart part in snejk.snakeParts)
                gameGrid.Children.Add(part.ellipse);
            snejk.setOnGrid();
        }

        private void paintApple()
        {
            switch (difficulty)
            {
                case "Medium":
                    if (applesEaten % 5 == 0) paintRotten();
                    break;
                case "Hard":
                    if (applesEaten % 3 == 0) paintRotten();
                    break;

            }

            newApple = new snakePart(random.Next(0, (int)gameGrid.Width / snakeSize), random.Next(0, (int)gameGrid.Height / snakeSize));

            ImageBrush myBrush = new ImageBrush();
            
            myBrush.ImageSource =
                new BitmapImage(new Uri("D:/pobrane/WPFsnejk/WPFsnejk/Resources/Images/apple.png"));

            newApple.ellipse.Fill = myBrush;
            apples.Add(newApple);
            gameGrid.Children.Add(newApple.ellipse);

            Grid.SetColumn(newApple.ellipse, newApple.X);
            Grid.SetRow(newApple.ellipse, newApple.Y);
        }

        private void paintRotten()
        {
            newRotten = new snakePart(random.Next(0, (int)gameGrid.Width / snakeSize), random.Next(0, (int)gameGrid.Height / snakeSize));

            ImageBrush myBrush = new ImageBrush();
            
            myBrush.ImageSource =
                new BitmapImage(new Uri("D:/pobrane/WPFsnejk/WPFsnejk/Resources/Images/trutka.png"));

            newRotten.ellipse.Fill = myBrush;
            rottens.Add(newRotten);
            gameGrid.Children.Add(newRotten.ellipse);

            Grid.SetColumn(newRotten.ellipse, newRotten.X);
            Grid.SetRow(newRotten.ellipse, newRotten.Y);
        }


        private void movementSwitch()
        {
            switch (movement)
            {
                case "up":
                    snejk.head.Y -= 1;
                    break;
                case "down":
                    snejk.head.Y += 1;
                    break;
                case "left":
                    snejk.head.X -= 1;
                    break;
                case "right":
                    snejk.head.X += 1;
                    break;
            }
        }

        private void Logic()
        {
            if (movement != "")
            {

                for (int i = snejk.snakeParts.Count - 1; i >= 1; i--)
                {
                    snejk.snakeParts[i].X = snejk.snakeParts[i - 1].X;
                    snejk.snakeParts[i].Y = snejk.snakeParts[i - 1].Y;
                }
                snejk.snakeParts[0].X = snejk.head.X;
                snejk.snakeParts[0].Y = snejk.head.Y;

                movementSwitch();

                if (ifWallHit())
                {
                    gameOver();
                }
                else
                {
                    selfColision();
                    ifAppleEaten();
                    ifRottenEaten();
                    snejk.setOnGrid();
                }
            }
        }

        private void pauseMenu()
        {
            timer.Stop();
            resumeGame.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Collapsed;
            pW.Visibility = Visibility.Visible;

        }

        private void gameOver()
        {
           
            SoundPlayer snd = new SoundPlayer("D:/pobrane/WPFsnejk/WPFsnejk/Resources/Sounds/koniec.wav");
            snd.Play();
            int x = ((MainWindow)App.Current.MainWindow).resultList.Items.Count;
            timer.Stop();
            ListBoxItem wynik = new ListBoxItem();
            wynik.Content = score + " points - on level " + difficulty;

            ((MainWindow)App.Current.MainWindow).resultList.Items.Add(wynik);

            ((MainWindow)App.Current.MainWindow).resultList.Items.SortDescriptions.Clear();
            ((MainWindow)App.Current.MainWindow).resultList.Items.SortDescriptions.Add(new SortDescription("Content", ListSortDirection.Descending));
            MessageBox.Show("GAME OVER! Your score: " + score);

            this.Close();

            ((MainWindow)App.Current.MainWindow).Visibility = Visibility.Visible;
        }

        private void ifAppleEaten()
        {
            for (int i = 0; i < apples.Count; i++)
            {
                if (snejk.head.X == apples[i].X && snejk.head.Y == apples[i].Y)
                {
                    snakePart newPart = new snakePart(snejk.snakeParts[snejk.snakeParts.Count - 1].X,
                    snejk.snakeParts[snejk.snakeParts.Count - 1].Y);

                    gameGrid.Children.Add(newPart.ellipse);
                    applesEaten++;
                    snejk.snakeParts.Add(newPart);
                    gameGrid.Children.Remove(apples[i].ellipse);
                    apples.Remove(apples[i]);
                    paintApple();

                    switch (difficulty)
                    {
                        case "Easy":
                            score += 10;
                            break;
                        case "Medium":
                            score += 25;
                            break;
                        case "Hard":
                            score += 50;
                            break;
                    }

                    scoreTextBlock.Text = score.ToString();
                    SoundPlayer snd = new SoundPlayer("D:/pobrane/WPFsnejk/WPFsnejk/Resources/Sounds/zjedzenie.wav");
                    snd.Play();
                }
            }
        }

        private void ifRottenEaten()
        {
            for (int i = 0; i < rottens.Count; i++)
            {
                if (snejk.head.X == rottens[i].X && snejk.head.Y == rottens[i].Y)
                {
                    gameOver();
                    this.Close();
                }
            }
        }

        private bool ifWallHit()
        {
            if (snejk.head.X < 0 || snejk.head.X > (gameGrid.Width / snakeSize) || snejk.head.Y < 0 || snejk.head.Y > (gameGrid.Height / snakeSize))
            {
                switch (difficulty)
                {
                    case "Easy":
                        noWalls();
                        return false;
                    case "Medium":
                        noWalls();
                        return false;
                    case "Hard":
                        SoundPlayer snd = new SoundPlayer("D:/pobrane/WPFsnejk/WPFsnejk/Resources/Sounds/kolizja.wav");
                        snd.Play();
                        System.Threading.Thread.Sleep(1300);
                        return true;
                }
            }
            return false;
        }

        private void selfColision()
        {
            foreach (snakePart snakePart in snejk.snakeParts)
            {
                if (snejk.head.X == snakePart.X && snejk.head.Y == snakePart.Y)
                {
                    SoundPlayer snd = new SoundPlayer("D:/pobrane/WPFsnejk/WPFsnejk/Resources/Sounds/kolizja.wav");
                    snd.Play();
                    gameOver();
                }
            }
        }

        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            if (timer.IsEnabled)
            {
                switch (e.Key)
                {
                    case Key.Left:
                        if (movement != "right")
                        {
                            movement = "left";
                            break;
                        }
                        break;
                    case Key.Up:
                        if (movement != "down")
                        {
                            movement = "up";
                            break;
                        }
                        break;

                    case Key.Right:
                        if (movement != "left")
                        {
                            movement = "right";
                            break;
                        }
                        break;
                    case Key.Down:
                        if (movement != "up")
                        {
                            movement = "down";
                            break;
                        }
                        break;
                    case Key.Escape:
                        pauseMenu();
                        break;
                }
            }
            else
            {
                movement = "";
                resumeGame.Visibility = Visibility.Collapsed;
                timer.Start();
            }
        }

        private void closingGame(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.Name == "mainWindow") window.Visibility = Visibility.Visible;
                if (window.Name == "pauseWin") window.Close();
            }
        }
    }
}
