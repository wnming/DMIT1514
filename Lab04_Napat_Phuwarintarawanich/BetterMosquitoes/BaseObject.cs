using Lesson05_Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterMosquitoes
{
    public class BaseObject
    {
        public Sprite Sprite;
        public ObjectTransform Transform;

        public BaseObject(Sprite sprite, ObjectTransform transform)
        {
            Sprite = sprite;
            Transform = transform;
        }

        public bool IsCollide(Rectangle otherBounds)
        {
            return Sprite.SpriteBounds.Intersects(otherBounds);
        }

        public void Move(Vector2 offset)
        {
            //check bounds
            Transform.TranslatePosition(offset);
            Sprite.UpdateSpriteBounds(Transform);
        }

        public void Update(GameTime gameTime)
        {
            //transform.CheckBounds(sprite);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch);
        }
    }

    public struct Sprite
    {
        public Texture2D SpriteSheet;
        public Rectangle SpriteBounds;
        public CelAnimationSequence AnimationSequence;
        public CelAnimationPlayer AnimationPlayer = new CelAnimationPlayer();

        public Sprite(Texture2D spriteSheet, Rectangle spriteBounds, int CelHeight, int CelWidth, int CelRow, int CelCount)
        {
            SpriteSheet = spriteSheet;
            SpriteBounds = spriteBounds;
            AnimationSequence = new CelAnimationSequence(spriteSheet, CelHeight, CelWidth, 1 / 9f, CelRow, CelCount);
            AnimationPlayer.Play(AnimationSequence);
        }

        public void Update(GameTime gameTime) 
        {
            AnimationPlayer.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            AnimationPlayer.Draw(spriteBatch, SpriteBounds.Location.ToVector2(), SpriteEffects.None);
        }

        public void UpdateSpriteBounds(ObjectTransform transform)
        {
            SpriteBounds = new Rectangle(transform.Position.ToPoint(), SpriteBounds.Size);
        }
    }

    public struct ObjectTransform
    {
        public Vector2 Position;
        public Vector2 Direction;
        //public float Rotation;

        public void TranslatePosition(Vector2 offsetVector)
        {
            Position += offsetVector;
        }

        public ObjectTransform(Vector2 position, Vector2 direction, float rotation)
        {
            this.Position = position;
            this.Direction = direction;
            //this.Rotation = rotation;
        }
    }
}
