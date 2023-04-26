using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformerGame
{
    public class GameObject: DrawableGameComponent
    {
        internal Transform _transform;
        internal Rectangle _rectangleBounds;
        internal Texture2D _texture;
        internal Vector2 Position => _transform.Position;


        // Each child should override/make a new spritebatch.
        // Objects of the same class can share the spritebatch.
        public static SpriteBatch spriteBatch;

        public Transform Transform => _transform;
        public Rectangle RectangleBounds => _rectangleBounds;
        public Texture2D Texture => _texture;
        public GameObject(Game game, Transform transform, Rectangle rectangle, Texture2D texture) : base(game)
        {
            if (spriteBatch is null)
            {
                spriteBatch = spriteBatch = new SpriteBatch(GraphicsDevice);
            }

            _transform = transform;
            _rectangleBounds = rectangle;
            _texture = texture;
            game.Components.Add(this);
        }
        //public override void Initialize()
        //{
        //    base.Initialize();
        //}
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

        }

        public override void Draw(GameTime gameTime)
        {
            //base.Draw(gameTime);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(_texture, _transform.Position, _texture.Bounds, Color.White, _transform.Rotation, _texture.Bounds.Center.ToVector2(), _transform.Scale, SpriteEffects.None, 0);
            spriteBatch.End();
        }
    }
}
