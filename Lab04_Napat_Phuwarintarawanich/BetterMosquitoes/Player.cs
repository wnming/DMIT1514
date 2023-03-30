using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMosquitoes
{
    public class Player : BaseObject
    {
        ObjectTransform Transform;
        Sprite SpriteSheet;
        PlayerControls Controls;
        Rectangle GameArea;

        private float Speed = 4f;

        PlayerState currentPlayerState = PlayerState.Alive;

        private bool leftPressed, rightPressed, firePressed;

        private List<PlayerBullet> PlayerBulletsList = new();

        Texture2D playerbulletTexture;
        Sprite bulletSprite;

        float cooldowntime = 0;

        public Player(Sprite sprite, ObjectTransform transform, PlayerControls controls, Rectangle gameArea) : base(sprite, transform)
        {
            Transform = transform;
            SpriteSheet = sprite;
            Controls = controls;
            GameArea = gameArea;
        }

        public void LoadContent(ContentManager content)
        {
            playerbulletTexture = content.Load<Texture2D>("octopus_ink");
            bulletSprite = new Sprite(playerbulletTexture, playerbulletTexture.Bounds, 26, 5, 0, 1, 1);
        }

        public void Update(GameTime gameTime)
        {
            switch (currentPlayerState)
            {
                case PlayerState.Alive:
                    SpriteSheet.Update(gameTime);
                    PlayerInput(Controls);
                    PlayerMove();
                    PlayerShoot(gameTime);
                    break;
                case PlayerState.Dying:
                    break;
                case PlayerState.Dead:
                    break;
                default:
                    break;
            }
            foreach (PlayerBullet bullet in PlayerBulletsList)
            {
                bullet.Update(gameTime);
            }
        }

        public void PlayerInput(PlayerControls controls)
        {
            rightPressed = Keyboard.GetState().IsKeyDown(controls.Left);
            leftPressed = Keyboard.GetState().IsKeyDown(controls.Right);
            firePressed = Keyboard.GetState().IsKeyDown(controls.Fire);
        }

        void PlayerMove()
        {
            if (leftPressed)
            {
                Transform.Direction = new Vector2(-1, 0);
                if (base.Transform.Position.X < GameArea.Left)
                {
                    Transform.Direction = Vector2.Zero;
                }
            }
            else
            {
                if (rightPressed)
                {
                    Transform.Direction = new Vector2(1, 0);
                    //90 is player rectangular - player transparent space
                    if (base.Transform.Position.X + 90 > GameArea.Right)
                    {
                        Transform.Direction = Vector2.Zero;
                    }
                }
                else
                {
                    Transform.Direction = Vector2.Zero;
                }
            }
            Move(Transform.Direction * Speed);
        }

        void PlayerShoot(GameTime gameTime)
        {
            cooldowntime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (firePressed && cooldowntime >= 400)
            {
                bool fire = false;
                //if (!isRest)
                //{

                //}
                PlayerBullet newBullet = new PlayerBullet(bulletSprite, new ObjectTransform());
                fire = newBullet.Fire(base.Transform.Position);
                PlayerBulletsList.Add(newBullet);
                cooldowntime = 0;
            }
        }

        public bool CheckBulletCollision(Rectangle enemyBound)
        {
            bool isCollide = false;
            foreach(PlayerBullet bullet in PlayerBulletsList)
            {
                if (bullet.IsCollide(enemyBound))
                {
                    bullet.Collide();
                    isCollide = true;
                }
            }
            return isCollide;
        }

        public void CheckPlayerCollision()
        {

        }

        void PlayerDie()
        {
            currentPlayerState = PlayerState.Dead;
        }

        public void DrawBullet(SpriteBatch spriteBatch)
        {
            switch (currentPlayerState)
            {
                case PlayerState.Alive:
                    break;
                case PlayerState.Dying:
                    break;
                case PlayerState.Dead:
                    break;
            }
            foreach (PlayerBullet bullet in PlayerBulletsList)
            {
                 bullet.Draw(spriteBatch);
            }
        }
    }

    public struct PlayerControls
    {
        public Keys Left;
        public Keys Right;
        public Keys Fire;

        public PlayerControls(Keys left, Keys right, Keys fire)
        {
            Left = left;
            Right = right;
            Fire = fire;
        }
    }

    public enum PlayerState
    {
        Alive,
        Dying,
        Dead
    }
}
