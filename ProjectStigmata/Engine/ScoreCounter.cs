using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectStigmata.Engine
{
    public class ScoreCounter 
    {
        private SpriteFont _font;
        private int _score;

        public ScoreCounter(SpriteFont font)
        {
            _font = font;
            _score = 0;
        }

        // Método de incremento do score
        public void Increase()
        {
            _score++;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, "Score: " + _score,Vector2.Zero, Color.White, 0f,Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
        }
    }
}
