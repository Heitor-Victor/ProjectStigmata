using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace ProjectStigmata
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _NGlogo;
        private Texture2D _STGlogo;
        private Texture2D _Pkey;
        private Vector2 _logoPosition;
        private Vector2 _PkeyPosition;
        private float _logoScale = 0.0f;
        private float _fadeSpeed = 0.01f;
        Menu _menu;
        private bool _fstScreen = false;
        private bool _menuScreen = false;
        private KeyboardState _previousKeyboardState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            _logoPosition = new Vector2((_graphics.PreferredBackBufferWidth - _NGlogo.Width) / 2.0f,
                (_graphics.PreferredBackBufferHeight - _NGlogo.Height) / 2.0f);
            _PkeyPosition = new Vector2((_graphics.PreferredBackBufferWidth - _Pkey.Width) / 2.0f,
                _STGlogo.Height);
    }

        protected override void LoadContent()
        {
            //800x480
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _NGlogo = Content.Load<Texture2D>("NGlogo");
            _STGlogo = Content.Load<Texture2D>("STGlogo");
            _Pkey = Content.Load<Texture2D>("Pkey");

            SpriteFont _font = Content.Load<SpriteFont>("t4cbeaulieux");
            List<string> _options = new List<string>()
            {
                "Novo jogo",
                "Opcoes",
                "Creditos"
            };
            _menu = new Menu(_font, _options);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _logoScale += _fadeSpeed;
            _logoScale = MathHelper.Clamp(_logoScale, 0.0f, 1.0f);

            if(_logoScale >= 1.0f)
            {
                _fadeSpeed *= -1.0f;
            }
            if (_logoScale <= 0.0f && _fadeSpeed < 0.0f)
            {
                _fstScreen = true;
                _fadeSpeed = 0.0f;
                _logoScale = -1.0f;
            }
            if (_fstScreen && Keyboard.GetState().IsKeyDown(Keys.Enter) && !_previousKeyboardState.IsKeyDown(Keys.Enter))
            {
                _fstScreen = false;
                _menuScreen = true;
            }
            _previousKeyboardState = Keyboard.GetState();

            if (_menuScreen)
            {
                _menu.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin();
            if (_logoScale > 0.0f)
            {
                _spriteBatch.Draw(_NGlogo, _logoPosition, Color.White * _logoScale);
            }
            if (_fstScreen)
            {
                _spriteBatch.Draw(_STGlogo,Vector2.Zero, Color.White);
                _spriteBatch.Draw(_Pkey,_PkeyPosition, Color.White);
            }
            if (_menuScreen)
            {
                _menu.Draw(_spriteBatch);
            }


            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
