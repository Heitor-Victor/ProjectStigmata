using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectStigmata.Engine
{
    public class Enemy : GameObject
    {
        private const float Speed = 2f; // Velocidade de movimento do inimigo

        private Player _player; // Referência para o jogador

        // Construtor da classe Enemy
        public Enemy(Texture2D image, Player player) : base(image)
        {
            _player = player; // Inicializa a referência para o jogador

            // Ajusta a posição inicial do inimigo para estar um pouco mais abaixo na tela
            _bounds.X = -100; // Define a posição horizontal inicial do inimigo fora da tela
            _bounds.Y = 260; // Define a posição vertical inicial do inimigo
        }

        // Método de atualização do inimigo
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime); // Chama o método de atualização da classe base (GameObject)

            // Calcula a direção do inimigo em relação ao jogador
            Vector2 direction = Vector2.Normalize(_player.Position - new Vector2(_bounds.X, _bounds.Y));

            // Move o inimigo em direção ao jogador com a velocidade definida
            Move(direction, Speed);
        }

        // Método para remover o inimigo do jogo
        public void Remove()
        {
            // Define a posição do inimigo fora da tela para que ele não seja desenhado
            _bounds.X = -1000;
            _bounds.Y = -1000;
        }
    }
}
