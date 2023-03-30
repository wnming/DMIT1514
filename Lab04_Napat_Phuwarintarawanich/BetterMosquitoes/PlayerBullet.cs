using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BetterMosquitoes
{
    public class PlayerBullet : BaseObject
    {
        private float BulletSpeed = 5f;
        public BulletState CurrentBulletState = BulletState.NotFlying;
  
        public PlayerBullet(Sprite sprite, ObjectTransform transform) : base(sprite, transform)
        {
            this.Sprite = sprite;
            this.Transform = transform;
        }

        public void Update(GameTime gameTime)
        {
            switch (CurrentBulletState)
            {
                case BulletState.Flying:
                    Move(new(0, -BulletSpeed));
                    break;
                case BulletState.NotFlying:
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (CurrentBulletState)
            {
                case BulletState.Flying:
                    //spriteBatch.Draw(Sprite.SpriteSheet, Transform.Position, Color.White);
                    base.Draw(spriteBatch);
                    break;
                case BulletState.NotFlying:
                    break;
            }
        }

        public bool Fire(Vector2 position)
        {
            bool fire = false;
            if (CurrentBulletState == BulletState.NotFlying)
            {
                fire = true;
                //make it center
                position.X += 20;
                this.Transform.TranslatePosition(position);
                CurrentBulletState = BulletState.Flying;
                Move(new(0, -BulletSpeed));
            }
            return fire;
        }

        public void Collide()
        {
            if(CurrentBulletState == BulletState.Flying)
            {
                CurrentBulletState = BulletState.NotFlying;
            }
        }
    }
}
