using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectStigmata.Engine
{
    public class GameObject
    {
        public Rectangle _bounds; // Retângulo delimitador do objeto
        protected Texture2D _image; // Textura do objeto

        // Propriedade para acessar o retângulo delimitador do objeto
        public Rectangle Bounds
        {
            get { return _bounds; }
        }

        // Propriedade para acessar e definir a posição horizontal do objeto
        public int X
        {
            get { return _bounds.X; }
            set { _bounds.X = value; }
        }

        // Propriedade para acessar e definir a posição vertical do objeto
        public int Y
        {
            get { return _bounds.Y; }
            set { _bounds.Y = value; }
        }

        // Propriedade para acessar e definir a posição do objeto como um ponto
        public Point Position
        {
            get { return _bounds.Location; }
            set { _bounds.Location = value; }
        }

        // Construtor da classe GameObject
        public GameObject(Texture2D image)
        {
            _image = image; // Define a textura do objeto
            _bounds = new Rectangle(0, 0, _image.Width, _image.Height); // Inicializa o retângulo delimitador com base nas dimensões da textura
        }

        // Propriedade indicando se o objeto está atacando
        public bool IsAttacking { get; private set; }

        // Método para iniciar o ataque do objeto
        public void Attack()
        {
            IsAttacking = true;
        }

        // Método para interromper o ataque do objeto
        public void StopAttack()
        {
            IsAttacking = false;
        }

        // Método para verificar a colisão com um inimigo e realizar ações correspondentes
        public void CheckEnemyCollision(Enemy enemy)
        {
            if (IsAttacking && Bounds.Intersects(enemy.Bounds))
            {
                enemy.Remove(); // Remove o inimigo da tela
            }
        }

        // Método virtual para inicializar o objeto (pode ser sobrescrito nas classes derivadas)
        public virtual void Initialize()
        {

        }

        // Método virtual para atualizar o objeto (pode ser sobrescrito nas classes derivadas)
        public virtual void Update(float deltaTime)
        {

        }

        // Método virtual para desenhar o objeto (pode ser sobrescrito nas classes derivadas)
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_image, _bounds, Color.White); // Desenha a textura do objeto na tela
        }

        // Método para calcular a direção do objeto em relação a outro objeto
        public Vector2 CalculateDirection(Vector2 targetPosition)
        {
            Vector2 currentPosition = new Vector2(_bounds.Center.X, _bounds.Center.Y); // Posição central do objeto
            return Vector2.Normalize(targetPosition - currentPosition); // Retorna a direção normalizada do objeto em relação ao alvo
        }

        // Método para mover o objeto com uma determinada velocidade na direção especificada
        public void Move(Vector2 direction, float speed)
        {
            _bounds.X += (int)(direction.X * speed); // Move o objeto horizontalmente
            _bounds.Y += (int)(direction.Y * speed); // Move o objeto verticalmente
        }
    }
}
