using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading;
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
using System.Media;
using System.Windows.Threading;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.LinkLabel;
namespace TeamProject
{
    public partial class MainWindow : Window
    {
        SoundPlayer Background = new SoundPlayer();
        DateTime date1 = new DateTime(0, 0);
        int playerSpeed = 2;
        int obstacleSpeed = 2;
        static int scoreToChange = 5;
        static int counter = 0;
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
        int score;
        int[] obstaclePosition = { 320, 310, 300, 305, 315 };

        public MainWindow()
        {
            InitializeComponent();
            MyCanvas.Focus();
            gameTimer.Tick += GameEngine;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            backgroundSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/background2.png"));
            background.Fill = backgroundSprite;
            background2.Fill = backgroundSprite;
            StartGame();
        }
        public void Write()
        {
            if (File.Exists("score.txt"))
            {
                string content = File.ReadAllText("score.txt");

            }
            string Score = score.ToString();
            File.WriteAllText("score.txt", Score);
        }
        public void Read()
        {
            string str = File.ReadAllText("score.txt");
            score = Convert.ToInt32(str);
        }
        public void WritePlayerSpeed()
        {
            if (File.Exists("score.txt"))
            {
                string content = File.ReadAllText("playerspeed.txt");

            }
            string PlSpeed = playerSpeed.ToString();
            File.WriteAllText("playerspeed.txt", PlSpeed);
        }
        public void ReadPlayerSpeed()
        {
            string str = File.ReadAllText("playerspeed.txt");
            playerSpeed = Convert.ToInt32(str);
        }
        public void WriteObstacleSpeed()
        {
            if (File.Exists("score.txt"))
            {
                string content = File.ReadAllText("obstaclespeed.txt");

            }
            string ObsSpeed = playerSpeed.ToString();
            File.WriteAllText("obstaclespeed.txt",ObsSpeed);
        }
        public void ReadObstacleSpeed()
        {
           string str = File.ReadAllText("obstaclespeed.txt");
            obstacleSpeed = Convert.ToInt32(str);
        }
        public void TimerGame(int secTime)
        {
            int countT = 0;
            for (int i = 0; i < secTime; i++)
            {
                Thread.Sleep(1000);
                countT++;
            }
        }
        private void GameEngine(object sender, EventArgs e)
        {
            Score Score = new Score();
            Score.curentPlayerSpeed = playerSpeed;
            Score.curentObstacleSpeed = obstacleSpeed;
            playerSpeed = Score.curentPlayerSpeed;
            obstacleSpeed = Score.curentObstacleSpeed;
            Canvas.SetLeft(background, Canvas.GetLeft(background) - playerSpeed);
            Canvas.SetLeft(background2, Canvas.GetLeft(background2) - playerSpeed);
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
                spriteIndex += .25;
                if (spriteIndex > 8)
                {
                    spriteIndex = 1;
                }
                RunSprite(spriteIndex);
            }
            if (jumping == true)
            {
                speed = -9;
                force -= 1;
            }
            else
                speed = 12;
            if (force < 0)
            {
                jumping = false;
            }
            if (Canvas.GetLeft(obstacle) < -50)
            {
                Canvas.SetLeft(obstacle, 950);
                Canvas.SetTop(obstacle, obstaclePosition[rnd.Next(0, obstaclePosition.Length)]);
                score += 1;
            }
            if (score==scoreToChange)
            {
                playerSpeed += 1;
                counter++;
                scoreToChange += 5;
            }
            if (playerHitBox.IntersectsWith(obstacleHitBox))
            {
                gameOver = true;
                gameTimer.Stop();
            }
            if (gameOver == true)
            {
                Write();
                WriteObstacleSpeed();
                WritePlayerSpeed();
                Score.death = true;
                HorrorQuest userForm = new HorrorQuest();
                Menu menu = new Menu();
                if (Score.EndGame_ == false)
                {
                    
                    userForm.ShowDialog();
                    this.Close();
                }
                else
                {
                    Background.Stop();
                    menu.ShowDialog();
                    this.Close();
                }
            }
            else
            {
                player.StrokeThickness = 0;
                obstacle.StrokeThickness = 0;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HorrorQuest userForm = new HorrorQuest();
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
            }
        }
        private void StartGame()
        {
            Background.Stream = Properties.Resources.strashnaja_muzika_na_hellouin_brainbug_nightmare_sinister_strings;
            Background.PlayLooping();
            Canvas.SetLeft(background, 0);
            Canvas.SetLeft(background2, 1262);
            Canvas.SetLeft(player, 262);
            Canvas.SetLeft(background2, 1262);
            Canvas.SetLeft(obstacle, 950);
            Canvas.SetTop(obstacle, 310);
            RunSprite(1);
            obstacleSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/obstacle.png"));
            obstacle.Fill = obstacleSprite;
            gameOver = false;
            jumping = false;
            if (Score.death == true)
            {
                Read();
                ReadObstacleSpeed();
                ReadPlayerSpeed();
            }
            else
            {
                score = 0;
            }
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
