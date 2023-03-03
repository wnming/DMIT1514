using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using MosquitoAttack;
using DMIT.GameObject;
using Microsoft.Xna.Framework.Input;

namespace Lab04_Napat_Phuwarintarawanich
{
    internal class Player: GameObject
    {
        Transform transform;
        Sprite sprite;
        Controls playerControls;

        //private Texture2D playerTexture;
        //private Vector2 playerPosition;
        //protected Vector2 PlayerDirection { get; set; }

        //change this
        CelAnimationSequence playerRunning;
        CelAnimationPlayer otterPlayer;

        private Rectangle gameArea;

        float playerSpeed = 4f;
        int maxPlayerHealth;
        int currentPlayerHealth;

        int maxPlayerAmmo;
        int currentPlayerAmmo;
        float fireRate;

        bool leftPressed, rightPressed, firePressed;

        PlayerState currentPlayerState = PlayerState.Alive;

        public Player(Sprite sprite, Transform transform, Controls controls) : base(sprite, transform)
        {
            this.sprite = sprite;
            this.transform = transform;
            this.playerControls = controls;
        }

        //public Player(Texture2D texture, Vector2 initialPosition, Rectangle gameArea, Controls controls) 
        //{
        //    playerControls = controls; 
        //    playerTexture = texture;
        //    playerPosition = initialPosition;
        //    this.gameArea = gameArea;
        //}

        //internal void Initialize(Vector2 initialPosition, Rectangle gameArea)
        //{
        //    playerPosition = initialPosition;
        //    this.gameArea = gameArea;
        //}

        //internal void LoadContent(ContentManager content)
        //{
        //    playerTexture = content.Load<Texture2D>("Cannon");
        //}

        internal void Update(GameTime gameTime)
        {
            switch (currentPlayerState)
            {
                case PlayerState.Alive:
                    //change this
                    //transform.Position += playerSpeed * transform.Direction * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    //if (playerPosition.X >= gameArea.Right - playerTexture.Height)
                    //{
                    //    playerPosition.X = gameArea.Right - playerTexture.Height;
                    //}
                    //if (playerPosition.X <= gameArea.Left)
                    //{
                    //    playerPosition.X = 0;
                    //}
                    PlayerInput(playerControls);
                    PlayerMove();
                    PlayerFire();
                    break;
                case PlayerState.Dying:
                    break;
                case PlayerState.Dead:
                    break;
                default:
                    break;
            }
        }

        public void PlayerInput(Controls controls)
        {
            rightPressed = Keyboard.GetState().IsKeyDown(controls.positiveDirection);
            leftPressed = Keyboard.GetState().IsKeyDown(controls.negativeDirection);
            firePressed = Keyboard.GetState().IsKeyDown(controls.wasFirePressedThisFrame);
        }

        public void PlayerMove()
        {
            if(leftPressed)
            {
                transform.Direction = new Vector2(-1, 0);
                //transform.Direction = Vector2.Zero;
            }
            else
            {
                if(rightPressed)
                {
                    transform.Direction = new Vector2(1, 0);
                }
                else
                {
                    transform.Direction = Vector2.Zero;
                }
                //else
                //{
                //    transform.Direction = new Vector2(-1, 0);
                //}
            }
            Move(transform.Direction * playerSpeed);

            //if (playerControls.positiveDirection)
            //{
            //    //move right
            //    Move(transform.Direction * playerSpeed);
            //}
            //if (playerControls.negativeDirection) 
            //{
            //    //move left
            //    Move(transform.Direction * playerSpeed);
            //}
        }

        public void PlayerFire()
        {
            //if (playerControls.wasFirePressedThisFrame)
            //{

            //}
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite.SpriteSheet, transform.Position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0f);
        }
    }

    internal struct Controls
    {
        //public Controls(bool pos, bool neg, bool fire) 
        //{ 
        //    positiveDirection = pos;
        //    negativeDirection = neg;
        //    wasFirePressedThisFrame = fire;
        //}

        public Controls(Keys pos, Keys neg, Keys fire)
        {
            this.positiveDirection = pos;
            this.negativeDirection = neg;
            this.wasFirePressedThisFrame = fire;
        }

        //public bool positiveDirection;
        //public bool negativeDirection;
        //public bool wasFirePressedThisFrame;

        public Keys positiveDirection;
        public Keys negativeDirection;
        public Keys wasFirePressedThisFrame;
    }

    //public struct InputRef
    //{

    //}

    public enum PlayerUpgradeState
    {
        None,

    }

    public enum PlayerState
    {
        Alive,
        Dying,
        Dead
    }
}
