using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectStigmata.Engine;
using System.Collections.Generic;

namespace ProjectStigmata.Screens
{
    public class GameScreen : IScreen
    {
        private GameObject _floor;
        private Player _player;
        private List<Texture2D> _playerAnimationFrames;

        public void LoadContent(ContentManager Content)
        {
            Texture2D floorTexture = Content.Load<Texture2D>("floorSprite");
            _floor = new GameObject(floorTexture);

            Texture2D playerTexture = Content.Load<Texture2D>("TristanIdle");

            // Criar uma lista para armazenar as texturas da animação
            _playerAnimationFrames = new List<Texture2D>();
            _playerAnimationFrames.Add(playerTexture); // Adicionar o frame de idle

            // Carregar as texturas dos frames de corrida (Run01, Run02, ..., Run06)
            for (int i = 1; i <= 6; i++)
            {
                Texture2D frameTexture = Content.Load<Texture2D>("Run" + i.ToString("00"));
                _playerAnimationFrames.Add(frameTexture);
            }

            // Criar uma instância do jogador e carregar as texturas da animação
            _player = new Player(playerTexture);
            _player.LoadAnimationFrames(_playerAnimationFrames);
            _player.Initialize();
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
