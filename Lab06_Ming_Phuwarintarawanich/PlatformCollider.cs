using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace PlatformerGame
{
    public class PlatformCollider
    {
        protected Texture2D colliderTexture;
        protected Vector2 position;
        protected Vector2 dimensions;
        internal Rectangle ColliderBox { get => new Rectangle((int)position.X, (int)position.Y, (int)dimensions.X, (int)dimensions.Y); }

        public PlatformCollider(Vector2 position, Vector2 dimensions)
        {
            this.position = position;
            this.dimensions = dimensions;
        }

        internal void LoadContent(ContentManager Content)
        {
            colliderTexture = Content.Load<Texture2D>("platform");
        }

        internal void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(colliderTexture, ColliderBox, new Rectangle(0, 0, 1, 1), color);
        }
    }
}
