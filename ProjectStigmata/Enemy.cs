using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectStigmata.Engine
{
    public class Enemy : GameObject
    {
        private const float Speed = 2f; // Velocidade de movimento do inimigo

        private Player _player; // Referência para o jogador
        private Vector2 _movementDirection; // Altere de object para Vector2
        private SpriteEffects _spriteEffect;
        private Texture2D _texture;

        // Construtor da classe Enemy
        public Enemy(Texture2D image, Player player) : base(image)
        {
            _player = player; // Inicializa a referência para o jogador
            _texture = image; // Atribui a textura recebida como parâmetro ao atributo _texture

            // Ajusta a posição inicial do inimigo para estar um pouco mais abaixo na tela
            _bounds.X = -100; // Define a posição horizontal inicial do inimigo fora da tela
            _bounds.Y = 260; // Define a posição vertical inicial do inimigo
            _movementDirection = new Vector2(1, 0); // Direita
            _spriteEffect = SpriteEffects.None;
        }

        // Método de atualização do inimigo
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime); // Chama o método de atualização da classe base (GameObject)

            // Verifica se o inimigo está na posição máxima à direita
            if (_bounds.Right >= 800)
            {
                // Se estiver na posição máxima, inverte a direção para esquerda
                _movementDirection = new Vector2(-1, 0); // Esquerda
            }
            // Verifica se o inimigo está na posição mínima à esquerda
            else if (_bounds.X <= 0)
            {
                // Se estiver na posição mínima, inverte a direção para direita
                _movementDirection = new Vector2(1, 0); // Direita
            }

            // Verifica se o inimigo está se movendo para a esquerda
            if (_movementDirection.X < 0)
            {
                // Se estiver se movendo para a esquerda, inverte a textura
                _spriteEffect = SpriteEffects.FlipHorizontally;
            }
            else
            {
                // Se não, mantém a textura como está
                _spriteEffect = SpriteEffects.None;
            }

            // Move o inimigo na direção atual com a velocidade definida
            Move(_movementDirection, Speed);
        }

        // Método para remover o inimigo do jogo
        public void Remove()
        {
            // Define a posição do inimigo fora da tela para que ele não seja desenhado
            _bounds.X = -1000;
            _bounds.Y = -1000;
        }

        // Método de desenho do inimigo
        public override void Draw(SpriteBatch spriteBatch)
        {
            // Desenha a textura do inimigo com base na orientação definida
            spriteBatch.Draw(_texture, _bounds, null, Color.White, 0f, Vector2.Zero, _spriteEffect, 0);
        }
    }
}