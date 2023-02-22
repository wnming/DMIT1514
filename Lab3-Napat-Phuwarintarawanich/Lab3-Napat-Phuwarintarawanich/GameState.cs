using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_Napat_Phuwarintarawanich
{
    internal enum GameState
    {
        Initialize,
        Start, 
        Serving, //serve a ball
        Playing, //playing until the ball goes off the border
        BallSticks, //allow ball to stick/not stick to paddle
        GameOver //display Gameover screen with player point and play again
    }
}
