using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace RacingCarGame.Models
{
    class IBase
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
    }
}
