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
using System.Windows.Shapes;

namespace RacingCarGame
{
    /// <summary>
    /// Interaction logic for StartGame.xaml
    /// </summary>
    public partial class StartGame : Window
    {
        public StartGame()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello this is Racing Game, where you can play for time."+"\n"+"If you want to play click on Lets go playing button.Then the game will start automaticly"+"\n"+"Now mo move the car you must click on <- or -> button"+"\n"+"If you will cut the another button the will be over"+"\n"+"You can also collect coins.What it gives? If you will collect coins for some period of time you may be hit into another cat and the game will not be over"+"\n"+"Enjoy this game))"+"\n"+"If you have some additionals questions please contact me via gmail:panas.oleh2121@gmail.com");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow gameWindow = new MainWindow();
            gameWindow.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}

