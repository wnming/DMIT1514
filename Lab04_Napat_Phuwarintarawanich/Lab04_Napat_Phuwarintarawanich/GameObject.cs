using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class GameObject
{
    public Sprite sprite;
    public Transform transform;

    public GameObject(Sprite sprite, Transform transform)
    {
        this.sprite = sprite;
        this.transform = transform;
    }

    public bool OnCollide(Rectangle otherBounds)
    {
        return sprite.Bounds.Intersects(otherBounds);
    }

    public void TranslatePosition(Vector2 offsetVector)
    {
        transform.Position = new Vector2(transform.Position.X + offsetVector.X, transform.Position.Y + offsetVector.Y);
    }

    public void Move(Vector2 offset)
    {
        //check bounds
        transform.TranslatePosition(offset);
        sprite.UpdateBounds(transform);
    }

    public void Update(GameTime gameTime)
    {
        //transform.CheckBounds(sprite);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        sprite.Draw(spriteBatch);
    }
}

public class Transform
{
    public Vector2 Position;
    public Vector2 Direction;
    public float Rotation;

    public Transform(Vector2 position, Vector2 direction, float rotation)
    {
        this.Position = position;
        this.Direction = direction;
        this.Rotation = rotation;
    }

    //public bool CheckBounds(Sprite sprite)
    //{
    //    return sprite.Bounds.X > sprite.GameArea.Width || sprite.Bounds.X < 0 || sprite.Bounds.Y < 0 || sprite.Bounds.Y > sprite.GameArea.Height;
    //}

    public void TranslatePosition(Vector2 offsetVector)
    {
        Position += offsetVector;
    }

    public Transform()
    {
    }
}

public struct Sprite
{
    public Texture2D SpriteSheet;
    public Rectangle Bounds;
    public float Scale;
    public Rectangle GameArea;

    public Sprite(Texture2D texture, Rectangle bounds, float scale, Rectangle gameArea)
    {
        this.SpriteSheet = texture;
        this.Bounds = bounds;
        this.Scale = scale;
        this.GameArea = gameArea;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(SpriteSheet, Bounds, Color.White);
    }

    public void UpdateBounds(Transform transform)
    {
        Bounds = new Rectangle(transform.Position.ToPoint(), Bounds.Size);
    }
}