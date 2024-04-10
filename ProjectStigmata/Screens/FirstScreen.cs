using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectStigmata.Engine;

namespace ProjectStigmata.Screens
{
    public class FirstScreen : IScreen
    {
        private GraphicsDeviceManager _graphics;
        private Texture2D _NGlogo;
        private Texture2D _STGlogo;
        private Texture2D _Pkey;
        private Vector2 _logoPosition;
        private Vector2 _PkeyPosition; 
        private float _logoScale = 0.0f;
        private float _fadeSpeed = 0.01f;
        private bool _STGscreen = false;


        public FirstScreen(GraphicsDeviceManager graphics)
        {
            _graphics = graphics;
        }

        public void LoadContent(ContentManager content)
        {
            _NGlogo = content.Load<Texture2D>("NGlogo");
            _STGlogo = content.Load<Texture2D>("STGlogo");
            _Pkey = content.Load<Texture2D>("Pkey");
        }

        public void Initialize()
        {
            _logoPosition = new Vector2((_graphics.PreferredBackBufferWidth - _NGlogo.Width) / 2.0f,
                    (_graphics.PreferredBackBufferHeight - _NGlogo.Height) / 2.0f);
            _PkeyPosition = new Vector2((_graphics.PreferredBackBufferWidth - _Pkey.Width) / 2.0f,
                    _STGlogo.Height);
        }

        public void Update(float deltaTime)
        {
            _logoScale += _fadeSpeed;
            _logoScale = MathHelper.Clamp(_logoScale, 0.0f, 1.0f);

            if (_logoScale >= 1.0f)
            {
                _fadeSpeed *= -1.0f;
            }
            if (_logoScale <= 0.0f && _fadeSpeed < 0.0f)
            {
                _fadeSpeed = 0.0f;
                _logoScale = -1.0f;
                _STGscreen = true;
            }

            if (_STGscreen && Input.getKeyDown(Keys.Enter))
            {
                Globals.GameInstance.ChangeScreen(EScreen.Menu);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_logoScale > 0.0f)
            {
                spriteBatch.Draw(_NGlogo, _logoPosition, Color.White * _logoScale);
            }
            if (_STGscreen)
            {
                spriteBatch.Draw(_STGlogo, Vector2.Zero, Color.White);
                spriteBatch.Draw(_Pkey, _PkeyPosition, Color.White);
            }
        }
    }
}
