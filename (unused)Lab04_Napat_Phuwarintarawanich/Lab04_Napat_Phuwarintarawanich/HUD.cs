using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Lab3_Napat_Phuwarintarawanich
{
    internal class HUD
    {
        private Vector2 hudPosition;
        private Texture2D hudTexture;
        private SpriteFont hudFont;

        private int PlayerHeart;
        private int EnemyCount;
        private int BulletCount;

        internal void Initialize(Vector2 position)
        {
            hudPosition = position;
        }

        internal void LoadContent(ContentManager content, SpriteFont font)
        {
            hudTexture = content.Load<Texture2D>("hud-bg");
            hudFont = font;
        }

        internal void Update(int playerHeart, int enemyCount, int bulletCount)
        {
            PlayerHeart = playerHeart;
            EnemyCount = enemyCount;
            BulletCount = bulletCount;
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(hudTexture, hudPosition, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0f);
            spriteBatch.DrawString(hudFont, PlayerHeart.ToString(), new Vector2(760, 570), Color.White);
            spriteBatch.DrawString(hudFont, BulletCount.ToString(), new Vector2(620, 570), Color.White);
            spriteBatch.DrawString(hudFont, EnemyCount.ToString(), new Vector2(75, 570), Color.White);
        }
    }
}
