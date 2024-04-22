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
        private List<Texture2D> _attackFrames;
        private Enemy _enemy;

        public void LoadContent(ContentManager Content)
        {
            // Carregar textura do chão
            Texture2D floorTexture = Content.Load<Texture2D>("floorSprite");
            _floor = new GameObject(floorTexture);

            // Carregar texturas de ataque
            _attackFrames = new List<Texture2D>();
            for (int i = 1; i <= 3; i++)
            {
                Texture2D attackTexture = Content.Load<Texture2D>("Atk" + i.ToString());
                _attackFrames.Add(attackTexture);
            }

            // Carregar texturas do jogador
            Texture2D playerTexture = Content.Load<Texture2D>("TristanIdle");
            _player = new Player(playerTexture);

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
            _player.LoadAttackFrames(_attackFrames); // Passar as texturas de ataque para o jogador
            _player.Initialize();

            // Carregar textura do inimigo
            Texture2D enemyTexture = Content.Load<Texture2D>("MobSpriteLeft");
            _enemy = new Enemy(enemyTexture, _player); // Passa a instância do jogador para o inimigo
            _enemy.Initialize();
        }

        public void Initialize()
        {

        }

        public void Update(float deltaTime)
        {
            // Atualize o jogador
            _player.Update(deltaTime);

            // Atualize o inimigo
            _enemy.Update(deltaTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Desenhe o chão
            _floor.Draw(spriteBatch);

            // Desenhe o jogador
            _player.Draw(spriteBatch);

            // Desenhe o inimigo
            _enemy.Draw(spriteBatch);
        }
    }
}
