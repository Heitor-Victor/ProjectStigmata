using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectStigmata.Engine;

namespace ProjectStigmata.Screens
{
    public class GameScreen : IScreen
    {
        private GameObject _floor;
        private Player _player;

        public void LoadContent(ContentManager Content)
        {
            Texture2D floorImage = Content.Load<Texture2D>("floorSprite");
            _floor = new GameObject(floorImage);

            Texture2D playerTexture = Content.Load<Texture2D>("Run01");
            _player = new Player(playerTexture); // Criação da instância de Player
            _player.Initialize(); // Inicialize o jogador
        }

        public void Initialize()
        {

        }

        public void Update(float deltaTime)
        {
            // Atualize o jogador
            _player.Update(deltaTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Desenhe o chão
            _floor.Draw(spriteBatch);

            // Desenhe o jogador
            _player.Draw(spriteBatch);
        }
    }
}
