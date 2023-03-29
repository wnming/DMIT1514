using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMosquitoes
{
    public class Enemy: BaseObject
    {
        ObjectTransform Transform;
        Sprite SpriteSheet;

        EnemyState currentEnemyState = EnemyState.Alive;

        private int Movement = 0;
        private int EnemyMovement = 20;
        private float Timer = 0;
        private float Speed = 1f;
        private float MoveTime = 0.5f;

        public Enemy(Sprite sprite, ObjectTransform transform) : base(sprite, transform)
        {
            SpriteSheet = sprite;
            Transform = transform;
        }
        public void Update(GameTime gameTime)
        {
            switch (currentEnemyState)
            {
                case EnemyState.Alive:
                    SpriteSheet.Update(gameTime);
                    EnemyMove(gameTime);
                    //PlayerFire();
                    break;
                case EnemyState.Dying:
                    break;
                case EnemyState.Dead:
                    break;
                default:
                    break;
            }
        }

        void EnemyMove(GameTime gameTime)
        {
            if (Movement == EnemyMovement)
            {
                Transform.Direction = new Vector2(0, 1);
                Movement = -1;
                Speed = -Speed;
                Timer = 0;
            }

            Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Timer > MoveTime && Movement != EnemyMovement)
            {
                Transform.Direction = new Vector2(Speed, 0);
                Timer = 0;
                Movement++;
            }
            Move(Transform.Direction);
        }
    }
    public enum EnemyState
    {
        Alive,
        Dying,
        Dead
    }

}
