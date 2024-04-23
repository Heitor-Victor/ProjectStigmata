using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ProjectStigmata.Engine;
using ProjectStigmata.Screens;
using System.Collections.Generic;

namespace ProjectStigmata
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private IScreen _firstScreen;
        private IScreen _menuScreen;
        private IScreen _currentScreen;
        private IScreen _gameScreen;
        private IScreen _gameOverScreen;
        private Song _menuSong;
        private Song _combatSong;


        public Main()
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
                case EScreen.Game:
                    _currentScreen = _gameScreen;
                    break;
                case EScreen.GameOver:
                    _currentScreen = _gameOverScreen;
                    break;
            }

            _currentScreen.Initialize();
        }

        protected override void Initialize()
        {
            base.Initialize();
            Globals.GameInstance = this;
            _currentScreen.Initialize();
            //MediaPlayer.Play(_menuSong);
        }

        protected override void LoadContent()
        {
            //800x480
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _menuSong = Content.Load<Song>("menuMusic");
            _combatSong = Content.Load<Song>("combatMusic");

            _firstScreen = new FirstScreen(_graphics);
            _firstScreen.LoadContent(Content);

            _menuScreen = new MenuScreen();
            _menuScreen.LoadContent(Content);

            _gameScreen = new GameScreen();
            _gameScreen.LoadContent(Content);

            _gameOverScreen = new GameOverScreen();
            _gameOverScreen.LoadContent(Content);

            _currentScreen = _firstScreen;

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                //Exit();

            _currentScreen.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            //mudança de música
/*            if (_currentScreen == _gameScreen && MediaPlayer.State == MediaState.Playing && MediaPlayer.Queue.ActiveSong == _menuSong)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(_combatSong);
            }*/

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
