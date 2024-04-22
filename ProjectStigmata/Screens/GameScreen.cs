using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ProjectStigmata.Engine;
using System.Collections.Generic;

namespace ProjectStigmata.Screens
{
    public class GameScreen : IScreen
    {
        private GameObject _floor; // Objeto representando o chão do jogo
        private Player _player; // Objeto representando o jogador
        private List<Texture2D> _playerAnimationFrames; // Lista de texturas para a animação do jogador
        private List<Texture2D> _attackFrames; // Lista de texturas para a animação de ataque
        private Enemy _enemy; // Objeto representando o inimigo
        private Texture2D _background; // Textura para o fundo do jogo

        // Função para carregar os recursos necessários para o jogo
        public void LoadContent(ContentManager Content)
        {
            // Carregar a textura do fundo
            _background = Content.Load<Texture2D>("gameBackground");

            // Carregar a textura do chão e criar um objeto GameObject com ela
            Texture2D floorTexture = Content.Load<Texture2D>("floorSprite");
            _floor = new GameObject(floorTexture);

            // Carregar as texturas de ataque e armazená-las em uma lista
            _attackFrames = new List<Texture2D>();
            for (int i = 1; i <= 3; i++)
            {
                Texture2D attackTexture = Content.Load<Texture2D>("Atk" + i.ToString());
                _attackFrames.Add(attackTexture);
            }

            // Carregar a textura do jogador e criar um objeto Player com ela
            Texture2D playerTexture = Content.Load<Texture2D>("TristanIdle");
            _player = new Player(playerTexture);

            // Criar uma lista para armazenar as texturas da animação do jogador
            _playerAnimationFrames = new List<Texture2D>();
            _playerAnimationFrames.Add(playerTexture); // Adicionar o frame de idle

            // Carregar as texturas dos frames de corrida do jogador
            for (int i = 1; i <= 6; i++)
            {
                Texture2D frameTexture = Content.Load<Texture2D>("Run" + i.ToString("00"));
                _playerAnimationFrames.Add(frameTexture);
            }

            // Carregar as texturas de animação e de ataque para o jogador
            _player.LoadAnimationFrames(_playerAnimationFrames);
            _player.LoadAttackFrames(_attackFrames);
            _player.Initialize();

            // Carregar a textura do inimigo e criar um objeto Enemy com ela
            Texture2D enemyTexture = Content.Load<Texture2D>("MobSpriteLeft");
            _enemy = new Enemy(enemyTexture, _player); // Passa a instância do jogador para o inimigo
            _enemy.Initialize();
        }

        // Função de inicialização, não utilizada neste caso
        public void Initialize()
        {

        }

        // Função de atualização que é chamada a cada quadro do jogo
        public void Update(float deltaTime)
        {
            // Atualizar o jogador
            _player.Update(deltaTime);

            // Atualizar o inimigo
            _enemy.Update(deltaTime);

            // Verificar se o jogador está atacando e se houve colisão com o inimigo
            if (_player.IsAttacking && _player.CheckCollision(_enemy))
            {
                // Se houve colisão enquanto o jogador estava atacando, remova o inimigo
                _enemy.Remove();
            }
        }

        // Função de desenho que é chamada a cada quadro do jogo
        public void Draw(SpriteBatch spriteBatch)
        {
            // Desenhar o background
            spriteBatch.Draw(_background, Vector2.Zero, Color.White);
            // Desenhar o chão
            _floor.Draw(spriteBatch);

            // Desenhar o jogador
            _player.Draw(spriteBatch);

            // Desenhar o inimigo
            _enemy.Draw(spriteBatch);
        }
    }
}
