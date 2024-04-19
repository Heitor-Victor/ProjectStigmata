using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectStigmata.Engine;
using System;
using System.Collections.Generic;

namespace ProjectStigmata.Screens
{
    internal class GameScreen : IScreen
    {
        private SpriteFont _font;

        public void LoadContent(ContentManager content)
        {
            _font = content.Load<SpriteFont>("t4cbeaulieux");
        }

        public void Initialize()
        {

        }

        public void Update(float deltaTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font,"Jogo aqui",Vector2.Zero,Color.Blue);
        }
    }
}
