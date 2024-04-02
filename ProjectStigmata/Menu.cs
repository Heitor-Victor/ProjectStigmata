using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;


namespace ProjectStigmata
{
     public class Menu
    {
        private SpriteFont _font;
        private List<string> _options;
        private int _selectedOption;
        private KeyboardState _previousKeyboardState;

        public Menu(SpriteFont font, List<string> options)
        {
            _font = font;
            _options = options;
            _selectedOption = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _options.Count; i++)
            {
                Color color = (_selectedOption == i) ? Color.Blue : Color.Black;
                spriteBatch.DrawString(_font, _options[i], new Vector2(100, 100 + i * 30), color);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && !_previousKeyboardState.IsKeyDown(Keys.Up))
            {
                _selectedOption = Math.Max(0, _selectedOption -1);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down) && !_previousKeyboardState.IsKeyDown(Keys.Down))
            {
                _selectedOption = Math.Min(_options.Count - 1, _selectedOption + 1);
            }
            //implementar seleção
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                string selectedOption = _options[_selectedOption];
                Console.WriteLine("opção selecionada: " +  selectedOption);
            }

            _previousKeyboardState = Keyboard.GetState();
        }
    }
}
