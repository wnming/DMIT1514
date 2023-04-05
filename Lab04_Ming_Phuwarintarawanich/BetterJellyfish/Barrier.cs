using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterJellyfish
{
    public class Barrier : BaseObject
    {
        public BarrierState CurrentBarrierState = BarrierState.Alive;
        public Barrier(Sprite sprite, ObjectTransform transform) : base(sprite, transform)
        {
            this.Sprite = sprite;
            this.Transform = transform;
        }

        public void Update(GameTime gameTime)
        {
            switch (CurrentBarrierState)
            {
                case BarrierState.Alive:
                    break;
                case BarrierState.Dead:
                    break;
                default:
                    break;
            }
        }

        public void CheckBarrierCollision(Rectangle enemyBound)
        {
            if (this.IsCollide(enemyBound) && CurrentBarrierState == BarrierState.Alive)
            {
                CurrentBarrierState = BarrierState.Dead;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (CurrentBarrierState)
            {
                case BarrierState.Alive:
                    base.Draw(spriteBatch);
                    break;
                case BarrierState.Dead:
                    break;
                default:
                    break;
            }
        }
    }

    public enum BarrierState
    {
        Alive,
        Dead
    }
}
