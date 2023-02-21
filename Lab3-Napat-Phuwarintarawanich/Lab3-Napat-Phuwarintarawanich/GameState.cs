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
        GameOver //check player poing and display play again screen
    }
}
