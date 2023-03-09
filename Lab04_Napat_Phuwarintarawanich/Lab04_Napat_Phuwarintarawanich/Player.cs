using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.Xna.Framework.Input;


public class Player: GameObject
{
    //public new Transform transform;
    //public new Sprite sprite;
    Transform transform;
    Sprite spriteSheet;
    Controls playerControls;

    //private Texture2D playerTexture;
    //private Vector2 playerPosition;
    //protected Vector2 PlayerDirection { get; set; }

    //change this
    //CelAnimationSequence playerRunning;
    //CelAnimationPlayer otterPlayer;

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
        this.transform = transform;
        this.sprite = sprite;
        playerControls = controls;
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

    public void Update(GameTime gameTime)
    {
        switch (currentPlayerState)
        {
            case PlayerState.Alive:
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

    void PlayerMove()
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

    void PlayerFire()
    {
        //if (playerControls.wasFirePressedThisFrame)
        //{

        //}
    }

    //public new void Draw(SpriteBatch spriteBatch)
    //{
    //    spriteBatch.Draw(sprite.SpriteSheet, transform.Position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0f);
    //}
}

public struct Controls
{
    //public Controls(bool pos, bool neg, bool fire) 
    //{ 
    //    positiveDirection = pos;
    //    negativeDirection = neg;
    //    wasFirePressedThisFrame = fire;
    //}
    public Keys positiveDirection;
    public Keys negativeDirection;
    public Keys wasFirePressedThisFrame;

    public Controls(Keys pos, Keys neg, Keys fire)
    {
        this.positiveDirection = pos;
        this.negativeDirection = neg;
        this.wasFirePressedThisFrame = fire;
    }
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
