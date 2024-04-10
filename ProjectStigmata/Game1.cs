using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectStigmata.Engine;
using ProjectStigmata.Screens;
using System.Collections.Generic;

namespace ProjectStigmata
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private IScreen _firstScreen;
        private IScreen _menuScreen;
        private IScreen _currentScreen;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        public void ChangeScreen(EScreen screenType)
        {
            switch (screenType)
            {
                case EScreen.Menu:
                    _currentScreen = _menuScreen;
                    break;
            }

            _currentScreen.Initialize();
        }

        protected override void Initialize()
        {
            base.Initialize();
            Globals.GameInstance = this;
            _currentScreen.Initialize();
    }

        protected override void LoadContent()
        {
            //800x480
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _firstScreen = new FirstScreen(_graphics);
            _firstScreen.LoadContent(Content);

            _menuScreen = new MenuScreen();
            _menuScreen.LoadContent(Content);

            _currentScreen = _firstScreen;

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _currentScreen.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            Input.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin();

            _currentScreen.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
