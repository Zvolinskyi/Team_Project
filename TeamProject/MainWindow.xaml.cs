using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
namespace TeamProject
{
    public partial class MainWindow : Window
    {
        DispatcherTimer gameTimer = new DispatcherTimer();
        Rect playerHitBox;
        Rect groundHitBox;
        Rect obstacleHitBox;
        bool jumping;
        int force = 20;
        int speed = 5;
        Random rnd = new Random();
        bool gameOver;
        double spriteIndex = 0;
        ImageBrush playerSprite = new ImageBrush();
        ImageBrush backgroundSprite = new ImageBrush();
        ImageBrush obstacleSprite = new ImageBrush();
        int[] obstaclePosition = { 320, 310, 300, 305, 315 };
        int score = 0;
        public MainWindow()
        {
            InitializeComponent();
            MyCanvas.Focus();
            gameTimer.Tick += GameEngine;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            backgroundSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/background.png"));
            background.Fill = backgroundSprite;
            background.Fill = backgroundSprite;
            StartGame();
        }
        private void GameEngine(object sender, EventArgs e)
        {
            Canvas.SetLeft(background, Canvas.GetLeft(background) - 18);
            Canvas.SetLeft(background2, Canvas.GetLeft(background2) - 18);
            if (Canvas.GetLeft(background) < -1262)
            {
                Canvas.SetLeft(background, Canvas.GetLeft(background2) + background2.Width);
            }
            if (Canvas.GetLeft(background2) < -1262)
            {
                Canvas.SetLeft(background2, Canvas.GetLeft(background) + background2.Width);
            }
            Canvas.SetTop(player, Canvas.GetTop(player) + speed);
            Canvas.SetLeft(obstacle, Canvas.GetLeft(obstacle) - 12);
            scoreText.Content = "Score: " + score;
            playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width - 15, player.Height);
            obstacleHitBox = new Rect(Canvas.GetLeft(obstacle), Canvas.GetTop(obstacle), obstacle.Width, obstacle.Height);
            groundHitBox = new Rect(Canvas.GetLeft(ground), Canvas.GetTop(ground), ground.Width, ground.Height);
            if (playerHitBox.IntersectsWith(groundHitBox))
            {
                speed = 0;
                Canvas.SetTop(player, Canvas.GetTop(ground) - player.Height);
                jumping = false;
                spriteIndex += .5;
                if (spriteIndex > 8)
                {
                    spriteIndex = 1;
                }
                RunSprite(spriteIndex);
            }
            }

            private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserForm userForm = new UserForm();
            userForm.ShowDialog();
        }
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter && gameOver == true)
            {
                StartGame();
            }
        }
        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Space && jumping == false && Canvas.GetTop(player) > 260)
            {
                jumping = true;
                force = 15;
                speed = -12;
                //playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/background.png"));
            }
        }
        private void StartGame()
        {
            Canvas.SetLeft(background, 0);
            Canvas.SetLeft(background2, 1262);
            Canvas.SetLeft(player, 262);
            Canvas.SetLeft(background2, 1262);
            Canvas.SetLeft(obstacle, 950);
            Canvas.SetTop(obstacle, 310);
            RunSprite(1);
            //obstacleSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/image/obstacle.png"));
            obstacle.Fill = obstacleSprite;
            jumping = false;
            gameOver = false;
            score = 0;
            scoreText.Content = "Score: " + score;
            gameTimer.Start();
        }
        private void RunSprite(double i)
        {
            switch(i)
            {
                case 1:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/1.png"));
                    break;
                case 2:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/2.png"));
                    break;
                case 3:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/3.png"));
                    break;
                case 4:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/4.png"));
                    break;
            }
            player.Fill = playerSprite;
        }
    }
}
