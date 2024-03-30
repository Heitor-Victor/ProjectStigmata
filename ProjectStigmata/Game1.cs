using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
            if (_logoScale == 0.0f)
            {
                _spriteBatch.Draw(_STGlogo,Vector2.Zero, Color.White);
                _spriteBatch.Draw(_Pkey,_PkeyPosition, Color.White);
            }


            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
