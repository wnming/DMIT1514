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

namespace Lab04_Napat_Phuwarintarawanich
{
    internal class Player
    {
        private Texture2D playerTexture;
        private Vector2 playerPosition;
        protected Vector2 PlayerDirection { get; set; }
        Controls playerControls;

        //change this
        CelAnimationSequence playerRunning;
        CelAnimationPlayer otterPlayer;

        private Rectangle gameArea;

        float playerSpeed = 400;
        int maxPlayerHealth;
        int currentPlayerHealth;

        int maxPlayerAmmo;
        int currentPlayerAmmo;
        float fireRate;

        PlayerState currentPlayerState = PlayerState.Alive;

        public Player(Texture2D texture, Vector2 initialPosition, Rectangle gameArea, Controls controls) 
        {
            playerControls = controls; 
            playerTexture = texture;
            playerPosition = initialPosition;
            this.gameArea = gameArea;
        }

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
                    playerPosition += playerSpeed * PlayerDirection * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (playerPosition.X >= gameArea.Right - playerTexture.Height)
                    {
                        playerPosition.X = gameArea.Right - playerTexture.Height;
                    }
                    if (playerPosition.X <= gameArea.Left)
                    {
                        playerPosition.X = 0;
                    }

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

        public void PlayerMove()
        {
            if (playerControls.positiveDirection)
            {
                //move right
            }
            if (playerControls.negativeDirection) 
            { 
                //move left
            }
        }

        public void PlayerFire()
        {
            if (playerControls.wasFirePressedThisFrame)
            {

            }
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerTexture, playerPosition, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0f);
        }
    }

    internal struct Controls
    {
        public Controls(bool pos, bool neg, bool fire) 
        { 
            positiveDirection = pos;
            negativeDirection = neg;
            wasFirePressedThisFrame = fire;
        }
        public bool positiveDirection;
        public bool negativeDirection;
        public bool wasFirePressedThisFrame;
    }

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
