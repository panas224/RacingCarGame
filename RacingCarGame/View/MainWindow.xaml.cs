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

namespace RacingCarGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer gametimer = new DispatcherTimer();
        List<Rectangle> itemRemover = new List<Rectangle>();
        Random random = new Random();
        ImageBrush playerImage = new ImageBrush();
        ImageBrush starImage = new ImageBrush();

        Rect playerHitBox;
        int speed = 15;
        int playerSpeed = 10;
        int CarNum;
        int starCounter = 30;
        int powerModeCounter = 200;
        double score;
        double i;
        bool moverLeft, moverRight, gameover, powerMode;

        public MainWindow()
        {
            InitializeComponent();
            MyCanvas.Focus();
            gametimer.Tick += GameLoop;
            gametimer.Interval = TimeSpan.FromMilliseconds(20);
            StartGame();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            score += .05;
            starCounter -= 1;
            scoreText.Content = "Survived" + score.ToString("#.#") + "Seconds";
            playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);
            if(moverLeft==true && Canvas.GetLeft(player) > 0)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) - playerSpeed);
            }
            if (moverRight == true && Canvas.GetLeft(player) + 90 < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) + playerSpeed);
            }
            if (starCounter<1)
            {
                MakeStar();
                starCounter = random.Next(600, 900);
            }
            foreach (var x in MyCanvas.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag=="roadMarks")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) + speed);
                    if (Canvas.GetTop(x)>510)
                    {
                        Canvas.SetTop(x, -152);
                    }
                }

                if ((string)x.Tag == "Car")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) + speed);
                    if (Canvas.GetTop(x) > 500)
                    {

                        ChangeCar(x);
                    }

                    Rect CarHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (playerHitBox.IntersectsWith(CarHitBox) && powerMode == true)
                    {
                        ChangeCar(x);
                    }
                    else if (playerHitBox.IntersectsWith(CarHitBox) && powerMode == false)
                    {
                        gametimer.Stop();
                        scoreText.Content += "Press Enter to restart the game";
                        gameover = true;
                    }
                }
                if ((string)x.Tag == "star")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) + 5);
                    Rect starHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (playerHitBox.IntersectsWith(starHitBox))
                    {
                        itemRemover.Add(x);
                        powerMode = true;
                        powerModeCounter = 200;
                    }
                    if (Canvas.GetTop(x) > 400)
                    {
                        itemRemover.Add(x);
                    }
                }
            }
            if (powerMode==true)
            {
                powerModeCounter -= 1;
                PowerUp();
                if (powerModeCounter<1)
                {
                    powerMode = false;
                }
                
            }
            else
            {
                playerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/playerImage.png"));
                MyCanvas.Background = Brushes.Gray;
            }
            foreach (Rectangle y in itemRemover)
            {
                MyCanvas.Children.Remove(y);
            }
            if(score>=10 && score<20)
            {
                speed = 12;
            }
            if (score>=20 && score <30)
            {
                speed = 14;
            }
            if (score >= 30 && score < 40)
            {
                speed = 16;
            }
            if (score >= 50 && score < 50)
            {
                speed = 18;
            }
            if (score >= 50 && score < 60)
            {
                speed = 20;
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.Left)
            {
                moverLeft = true;
            }
            if (e.Key==Key.Right)
            {
                moverRight = true;
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                moverLeft = false;
            }
            if (e.Key == Key.Right)
            {
                moverRight = false;
            }
            if (e.Key==Key.Enter && gameover==true )
            {
                StartGame();
            }
        }
        private void StartGame()
        {
            speed = 8;
            gametimer.Start();
            moverLeft = false;
            moverRight = false;
            gameover = false;
            score = 0;
            scoreText.Content = "Survived 0 seconds ";
            playerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/playerImage.png"));
            starImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/star.png"));
            player.Fill = playerImage;
            MyCanvas.Background = Brushes.Gray;
            foreach (var x in MyCanvas.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag=="Car")
                {
                    Canvas.SetTop(x, (random.Next(100, 400) * -1));
                    Canvas.SetLeft(x, random.Next(0,430));
                    ChangeCar(x);
                }
                if ((string) x.Tag == "star")
                {
                    itemRemover.Add (x);
                }
            }
            itemRemover.Clear();
        }

        private void ChangeCar(Rectangle car )
        {
            CarNum = random.Next(1, 6);
            ImageBrush carImage = new ImageBrush();
            switch(CarNum)
            {
                case 1:
                    carImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/car1.png"));
                    break;
                case 2:
                    carImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/car2.png"));
                    break;
                case 3:
                    carImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/car3.png"));
                    break;
                case 4:
                    carImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/car4.png"));
                    break;
                case 5:
                    carImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/car5.png"));
                    break;
                case 6:
                    carImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/car6.png"));
                    break;
            }
            car.Fill = carImage;
            Canvas.SetTop(car, (random.Next(100, 400) * -1));
            Canvas.SetLeft(car, random.Next(0, 430));

        }
        private void PowerUp()
        {
            i += .5;
            if (i>4)
            {
                i = 1;
            }
            switch (i)
            {
                case 1:
                    playerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/powermode1.png"));
                    break;
                case 2:
                    playerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/powermode2.png"));
                    break;
                case 3:
                    playerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/powermode3.png"));
                    break;
                case 4:
                    playerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/powermode4.png"));
                    break;
            }
            MyCanvas.Background = Brushes.LightCoral;
        }
        private void MakeStar()
        {
            Rectangle newStar = new Rectangle
            {
                Height = 50,
                Width = 50,
                Tag = "star",
                Fill = starImage
            };
            Canvas.SetLeft(newStar,random.Next(0,430));
            Canvas.SetTop(newStar, (random.Next(100, 400) * -1));
            MyCanvas.Children.Add(newStar);
        }

    }
}
