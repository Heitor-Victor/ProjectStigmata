using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectStigmata.Engine;
using System;
using System.Collections.Generic;

namespace ProjectStigmata.Screens
{
    public class CreditScreen : IScreen
    {
        private SpriteFont _font;
        private List<string> _options;
        private Texture2D _tristanAnimation;
        private Rectangle[] _frames;
        private int _index;
        private double _time;

        public void LoadContent(ContentManager content)
        {
            _tristanAnimation = content.Load<Texture2D>("tristanAnimation");

            _font = content.Load<SpriteFont>("t4cbeaulieux");

            _options = new List<string>()
            {
                "Alexandre Monteiro",
                "Heitor Victor",
                "Igor Oliveira",
                "Victor Giovani"
            };
        }
        public void Initialize()
        {
            _frames = new Rectangle[48];

            int startX = 0;
            int startY = 0;
            int width = 800;
            int height = 480;
            for (int i = 0; i < _frames.Length; i++)
            {
                _frames[i] = new Rectangle(startX, startY, width, height);
                startX += width;
                if (startX == 9600)
                {
                    startX = 0;
                    startY += 480;
                }
            }
            _index = 0;
            _time = 0.0f;
        }
        public void Update(float deltaTime)
        {
            _time = _time + deltaTime;
            if (_time > 0.1f)
            {
                _time = 0.0f;
                _index++;
                if (_index >= _frames.Length)
                {
                    _index = 0;
                }
            }

            if (Input.getKeyDown(Keys.Escape))
            {
                Globals.GameInstance.ChangeScreen(EScreen.Menu);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_tristanAnimation, new Rectangle(0, 0, 800, 480), _frames[_index], Color.White);
            for (int i = 0; i < _options.Count; i++)
            {
                spriteBatch.DrawString(_font, _options[i], new Vector2(100, 100 + i * 40), Color.Black, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
            }
        }
    }
}
