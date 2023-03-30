using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
        private int EnemyMovement = 13;
        private float Timer = 0;
        private float Speed = 0.9f;
        private float MoveTime = 0.5f;

        private List<EnemyBullet> EnemyBulletsList = new();

        Texture2D enemybulletTexture;
        Sprite bulletSprite;

        Random random = new Random();

        float cooldowntime = 0;

        public Enemy(Sprite sprite, ObjectTransform transform) : base(sprite, transform)
        {
            SpriteSheet = sprite;
            Transform = transform;
        }
        public void LoadContent(ContentManager content)
        {
            enemybulletTexture = content.Load<Texture2D>("jellyfish-laser");
            bulletSprite = new Sprite(enemybulletTexture, enemybulletTexture.Bounds, 40, 8, 0, 1, 1);
        }

        public void Update(GameTime gameTime)
        {
            switch (currentEnemyState)
            {
                case EnemyState.Alive:
                    SpriteSheet.Update(gameTime);
                    EnemyMove(gameTime);
                    EnemyShoot(gameTime);
                    //PlayerFire();
                    break;
                case EnemyState.Dying:
                    break;
                case EnemyState.Dead:
                    break;
                default:
                    break;
            }
            foreach (EnemyBullet bullet in EnemyBulletsList)
            {
                bullet.Update(gameTime);
            }
        }

        void EnemyMove(GameTime gameTime)
        {
            if (Movement == EnemyMovement)
            {
                Transform.Direction = new Vector2(0, 1);
                Movement = 0;
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

        void EnemyShoot(GameTime gameTime)
        {
            //fix shoot time
            cooldowntime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            int randomTimeShoot = random.Next(3000, 10000);
            if (cooldowntime >= randomTimeShoot)
            {
                bool fire = false;
                //if (!isRest)
                //{

                //}
                EnemyBullet newBullet = new EnemyBullet(bulletSprite, new ObjectTransform());
                fire = newBullet.Fire(base.Transform.Position);
                EnemyBulletsList.Add(newBullet);
                cooldowntime = 0;
            }
        }

        public void DrawEnemyBullet(SpriteBatch spriteBatch)
        {
            switch (currentEnemyState)
            {
                case EnemyState.Alive:
                    break;
                case EnemyState.Dying:
                    break;
                case EnemyState.Dead:
                    break;
            }
            foreach (EnemyBullet bullet in EnemyBulletsList)
            {
                bullet.Draw(spriteBatch);
            }
        }
    }
    public enum EnemyState
    {
        Alive,
        Dying,
        Dead
    }

}
