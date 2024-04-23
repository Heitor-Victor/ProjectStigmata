using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectStigmata.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectStigmata.Screens
{
    public class GameOverScreen : IScreen
    {
        private Texture2D _gameOver;

        public void LoadContent(ContentManager content)
        {
            _gameOver = content.Load<Texture2D>("You_Died");
        }

        public void Initialize()
        {

        }

        public void Update(float deltaTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_gameOver, Vector2.Zero, Color.White);
        }
    }
}
