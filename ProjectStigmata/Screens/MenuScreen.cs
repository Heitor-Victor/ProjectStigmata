﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectStigmata.Engine;
using System;
using System.Collections.Generic;

namespace ProjectStigmata.Screens
{
    public class MenuScreen : IScreen
    {
        private SpriteFont _font;
        private List<string> _options;
        private int _selectedOption = 0;
        private Texture2D _menuGif;

        public void LoadContent(ContentManager content)
        {
            _font = content.Load<SpriteFont>("t4cbeaulieux");
            
            _options = new List<string>()
            {
                "New game",
                "Options",
                "Credits"
            };
            _menuGif = content.Load<Texture2D>("TristanMenu");
        }

        public void Initialize()
        {

        }

        public void Update(float deltaTime)
        {
            if (Input.getKeyDown(Keys.Up))
            {
                _selectedOption = Math.Max(0, _selectedOption - 1);
            }
            else if (Input.getKeyDown(Keys.Down))
            {
                _selectedOption = Math.Min(_options.Count - 1, _selectedOption + 1);
            }
            //implementar seleção
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                string selectedOption = _options[_selectedOption];
                Console.WriteLine("opção selecionada: " + selectedOption);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_menuGif, Vector2.Zero, Color.White);
            
            for (int i = 0; i < _options.Count; i++)
            {
                Color color = (_selectedOption == i) ? Color.Blue : Color.Black;
                spriteBatch.DrawString(_font, _options[i], new Vector2(100, 100 + i * 40), color, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
            }
        }
    }
}
