using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_Napat_Phuwarintarawanich
{
    public class Winner
    {
        public Winner()
        {
            _WinTile = Tile.TileState.Blank;
        }

        public Tile.TileState _WinTile { get; private set; }

        public bool CheckTheWinner(int c1, int c2, int c3, int r1, int r2, int r3, Tile[,] gameBoard)
        {
            if(gameBoard[c1, r1]._TileState == gameBoard[c2, r2]._TileState && gameBoard[c2, r2]._TileState  == gameBoard[c3, r3]._TileState && gameBoard[c1, r1]._TileState != Tile.TileState.Blank)
            {
                _WinTile = gameBoard[c1, r1]._TileState;
                return true;
            }
            return false;
        }
    }
}
