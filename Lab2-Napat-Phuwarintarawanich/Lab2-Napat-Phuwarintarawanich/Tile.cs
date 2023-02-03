using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Lab2_Napat_Phuwarintarawanich.Tile;
using Color = Microsoft.Xna.Framework.Color;

namespace Lab2_Napat_Phuwarintarawanich
{
    public class Tile
    {
        public enum TileState
        {
            Blank,
            X,
            O
        }
        public Rectangle _Rectangle { get; private set; }

        public TileState _TileState { get; private set; }

        public Tile(Rectangle rectangle, TileState tileState)
        {
            _Rectangle = rectangle;
            _TileState = tileState;
        }

        public Tile(Rectangle rectangle)
        {
            _Rectangle = rectangle;
            _TileState = TileState.Blank;
        }

        public void Reset()
        {
            _TileState = TileState.Blank;
        }

        public void Setstate(TileState state)
        {
            _TileState = state;
        }

        public bool TrySetState(Point point, TileState tileState)
        {
            if(_TileState == TileState.Blank && _Rectangle.Contains(point))
            {
                Setstate(tileState);
                return true;
            }
            return false;
        }
    }
}
