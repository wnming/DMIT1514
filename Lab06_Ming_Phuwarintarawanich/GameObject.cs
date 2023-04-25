using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformerGame
{
    public class GameObject: DrawableGameComponent
    {
        Rectangle rectangle;
        Transform transform;
        Texture2D texture;
        // Each child should overrode/make a new spritebatch.
        // Objects of the same class can share the spritebatch.
        public static SpriteBatch spriteBatch;

        public GameObject(Game game) : base(game)
        {
            // Add more to the constructor.
            if (spriteBatch is null)
            {
                spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            }
            game.Components.Add(this); // This allows the game to call Update and Draw automatically.
        }

        public void Start(Vector2 startPosition)
        {
            // Use this to "reset" your game object at a position. Add more if needed.
            transform._position = startPosition;
            Enabled = true;
            Visible = true;
        }

        // This will be run by the game automatically if "Enabled" is true.
        public override void Update(GameTime gameTime)
        {
            // After intializing your transform, use transform.MovePosition() to move.
            base.Update(gameTime);
        }

        // This will be run by the game automatically if "Visible" is true;
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            //spriteBatch.Draw(texture, transform._position, texture.Bounds, Color.White, transform._rotation, texture.Bounds.Center.ToVector2(), transform._scale, SpriteEffects.None, 0);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
