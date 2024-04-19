using Microsoft.Xna.Framework;
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
        private List<string> _names;
        private List<string> _commands;
        private int _selectedOption = 0;
        private Texture2D _tristanAnimation;
        private Rectangle[] _frames;
        private int _index;
        private double _time;
        private bool[] _exibition;

        public void LoadContent(ContentManager content)
        {
            _font = content.Load<SpriteFont>("t4cbeaulieux");
            _tristanAnimation = content.Load<Texture2D>("tristanAnimation");
            _options = new List<string>()
            {
                "New game",
                "Controls",
                "Credits",
                "Exit"
            };
            _names = new List<string>()
            {
                "Alexandre Monteiro",
                "Heitor Victor",
                "Igor Oliveira",
                "Victor Giovani"
            };
            _commands = new List<string>()
            {
                "Move : arrows",
                "Attack : x"
            };
        }

        public void Initialize()
        {
            //lógica dos frames
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

            //lógica da exibição
            _exibition = new bool[3];
            _exibition[0] = true; //tela menu
            _exibition[1] = false; // tela controles
            _exibition[2] = false; // tela créditos
        }

        public void Update(float deltaTime)
        {
            //lógica animação (contínua)
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
            
            if (_exibition[0] == true)
            {
            //lógica das opções do menu
                if (Input.getKeyDown(Keys.Up))
                {
                    _selectedOption = Math.Max(0, _selectedOption - 1);
                }
                else if (Input.getKeyDown(Keys.Down))
                {
                    _selectedOption = Math.Min(_options.Count - 1, _selectedOption + 1);
                }
                // Implementar seleção
                if (Input.getKeyDown(Keys.Enter))
                {
                    string selectedOption = _options[_selectedOption];
                    if (selectedOption == "Exit")
                    {
                        // Chama o Environment.Exit para sair do jogo - byVictor                   
                        Environment.Exit(0);
                    } else if (selectedOption == "Controls")
                    {
                        _exibition[0] = false;
                        _exibition[1] = true;
                    } else if (selectedOption == "Credits")
                    {
                        _exibition[0] = false;
                        _exibition[2] = true;
                    } else if (selectedOption == "New game")
                    {
                        Globals.GameInstance.ChangeScreen(EScreen.Game);
                    }
                }
            }
            if (_exibition[1] ==  true)
            {
                if (Input.getKeyDown(Keys.Escape))
                {
                    _exibition[0] = true;
                    _exibition[1] = false;
                }
            }
            if (_exibition[2]  == true)
            {
                if (Input.getKeyDown(Keys.Escape))
                {
                    _exibition[0] = true;
                    _exibition[2] = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_tristanAnimation, new Rectangle(0, 0, 800, 480), _frames[_index], Color.White);

            if (_exibition[0] ==  true)
            {
                for (int i = 0; i < _options.Count; i++)
                {
                    Color color = (_selectedOption == i) ? Color.Blue : Color.Black;
                    spriteBatch.DrawString(_font, _options[i], new Vector2(100, 100 + i * 40), color, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                }
            }

            if (_exibition[1] == true)
            {
                for (int i = 0; i < _commands.Count; i++)
                {
                    spriteBatch.DrawString(_font, _commands[i], new Vector2(100, 100 + i * 40), Color.Black, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                }
            }

            if (_exibition[2] == true)
            {
                for (int i = 0; i < _names.Count; i++)
                {
                    spriteBatch.DrawString(_font, _names[i], new Vector2(100, 100 + i * 40), Color.Black, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                }
            }
        }
    }
}
