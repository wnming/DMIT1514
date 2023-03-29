using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMosquitoes
{
    public class Player: BaseObject
    {
        ObjectTransform Transform;
        Sprite SpriteSheet;
        PlayerControls Controls;
        Rectangle GameArea;

        private float Speed = 4f;

        PlayerState currentPlayerState = PlayerState.Alive;

        private bool leftPressed, rightPressed, firePressed;

        public Player(Sprite sprite, ObjectTransform transform, PlayerControls controls, Rectangle gameArea) : base(sprite, transform)
        {
            Transform = transform;
            SpriteSheet = sprite;
            Controls = controls;
            GameArea = gameArea;
        }

        public void Update(GameTime gameTime)
        {
            switch (currentPlayerState)
            {
                case PlayerState.Alive:
                    SpriteSheet.Update(gameTime);
                    PlayerInput(Controls);
                    PlayerMove();
                    //PlayerFire();
                    break;
                case PlayerState.Dying:
                    break;
                case PlayerState.Dead:
                    break;
                default:
                    break;
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
                if(base.Transform.Position.X < GameArea.Left)
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
