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

        internal string HighestSide { get; set; }
        internal int HighestScore { get; set; } = 0;
        internal int LeftHit { get; set; } = 0;
        internal int RightHit { get; set; } = 0; 

        internal void Initialize(Vector2 position)
        {
            hudPosition = position;
        }

        internal void Reset()
        {
            LeftHit = 0;
            RightHit = 0;
        }

        internal void LoadContent(ContentManager content)
        {
            hudTexture = content.Load<Texture2D>("HUD-background");
            hudFont = content.Load<SpriteFont>("AnjaEliane");
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(hudTexture, hudPosition, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0f);
            Vector2 totalHitCenter = hudFont.MeasureString(HighestScore.ToString()) / 2f;
            spriteBatch.DrawString(hudFont, "High Score: " + HighestScore.ToString(), new Vector2(300, 580), Color.DarkViolet, 0, totalHitCenter, 2.0f, SpriteEffects.None, 0);

            Vector2 leftHitCenter = hudFont.MeasureString(LeftHit.ToString()) / 2f;
            spriteBatch.DrawString(hudFont, LeftHit.ToString(), new Vector2(70, 580), Color.Black, 0, leftHitCenter, 2.0f, SpriteEffects.None, 0);

            Vector2 rightHitCenter = hudFont.MeasureString(RightHit.ToString()) / 2f;
            spriteBatch.DrawString(hudFont, RightHit.ToString(), new Vector2(730, 580), Color.Black, 0, rightHitCenter, 2.0f, SpriteEffects.None, 0);

        }

        internal void SetHighScore(int score, string side)
        {
            if (score > HighestScore)
            {
                HighestScore = score;
                HighestSide = side;
            }
            else
            {
                if(score == HighestScore)
                {
                    HighestSide = "tie";
                }
            }
        }
    }
}
