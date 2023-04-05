using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterJellyfish
{
    public class Player : BaseObject
    {
        ObjectTransform Transform;
        Sprite SpriteSheet;
        PlayerControls Controls;
        Rectangle GameArea;


        private float Speed = 4f;

        public int MaxLoadBullet = 15;
        public int CurrentHeart { get; set; }
        public int PlayerScore { get; set; }
        public int ReloadBullet { get; set; }

        public PlayerState CurrentPlayerState = PlayerState.Alive;

        private bool leftPressed, rightPressed, firePressed;

        private List<PlayerBullet> PlayerBulletsList = new();

        Texture2D playerbulletTexture;
        Sprite bulletSprite;

        float cooldowntime = 0;

        private bool IsBuddy;
        private bool BuddyDirectionLeft = true;

        public Player(Sprite sprite, ObjectTransform transform, PlayerControls controls, Rectangle gameArea, bool isBuddy) : base(sprite, transform)
        {
            Transform = transform;
            SpriteSheet = sprite;
            Controls = controls;
            GameArea = gameArea;
            ReloadBullet = MaxLoadBullet;
            IsBuddy = isBuddy;
        }

        public void Reset(int maxHeart)
        {
            CurrentHeart = maxHeart;
            PlayerScore = 0;
              ReloadBullet = MaxLoadBullet;
            CurrentPlayerState = PlayerState.Alive;
        }

        public void LoadContent(ContentManager content)
        {
            playerbulletTexture = content.Load<Texture2D>(IsBuddy ? "buddy-bullet" : "octopus_ink");
            bulletSprite = new Sprite(playerbulletTexture, playerbulletTexture.Bounds, 26, 5, 0, 1, 1);
        }

        public void Update(GameTime gameTime)
        {
            switch (CurrentPlayerState)
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
            if (IsBuddy)
            {
                //90 is player rectangular - player transparent space
                if (base.Transform.Position.X < GameArea.Left)
                {
                    BuddyDirectionLeft = false;
                }
                if (base.Transform.Position.X + 90 > GameArea.Right)
                {
                    BuddyDirectionLeft = true;
                }
                if (BuddyDirectionLeft)
                {
                    Transform.Direction = new Vector2(-1, 0);
                }
                else
                {
                    Transform.Direction = new Vector2(1, 0);
                }
                Speed = 3f;
            }
            else
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
            }
            Move(Transform.Direction * Speed);
        }

        void PlayerShoot(GameTime gameTime)
        {
            if (IsBuddy)
            {
                cooldowntime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (cooldowntime >= 1200)
                {
                    bool fire = false;
                    PlayerBullet newBullet = new PlayerBullet(bulletSprite, new ObjectTransform());
                    fire = newBullet.Fire(base.Transform.Position);
                    PlayerBulletsList.Add(newBullet);
                    cooldowntime = 0;
                    ReloadBullet -= 1;
                }
            }
            else
            {
                if (!IsReloadBullet())
                {
                    cooldowntime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (firePressed && cooldowntime >= 400)
                    {
                        bool fire = false;
                        PlayerBullet newBullet = new PlayerBullet(bulletSprite, new ObjectTransform());
                        fire = newBullet.Fire(base.Transform.Position);
                        PlayerBulletsList.Add(newBullet);
                        cooldowntime = 0;
                        ReloadBullet -= 1;
                    }
                }
            }
        }

        public bool CheckBulletCollision(Rectangle enemyBound, Barrier barrier)
        {
            bool isCollide = false;
            foreach(PlayerBullet bullet in PlayerBulletsList)
            {
                if (bullet.CurrentBulletState == BulletState.Flying && bullet.IsCollide(enemyBound))
                {
                    bullet.Collide();
                    isCollide = true;
                    PlayerScore += 1;
                }
                if (bullet.CurrentBulletState == BulletState.Flying && bullet.IsCollide(barrier.Sprite.SpriteBounds) && barrier.CurrentBarrierState == BarrierState.Alive)
                {
                    bullet.Collide();
                }
            }
            return isCollide;
        }

        public void CheckPlayerCollision(Rectangle enemyBound)
        {
            PlayerDie();
        }

        public void PlayerDie()
        {
            CurrentPlayerState = PlayerState.Dead;
        }

        public bool IsReloadBullet()
        {
            return ReloadBullet <= 0;
        }

        public void DrawBullet(SpriteBatch spriteBatch)
        {
            switch (CurrentPlayerState)
            {
                case PlayerState.Alive:
                    foreach (PlayerBullet bullet in PlayerBulletsList)
                    {
                        bullet.Draw(spriteBatch);
                    }
                    break;
                case PlayerState.Dying:
                    break;
                case PlayerState.Dead:
                    break;
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
