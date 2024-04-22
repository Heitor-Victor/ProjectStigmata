using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectStigmata.Engine
{
    public class GameObject
    {
        protected Rectangle _bounds;
        protected Texture2D _image;

        public Rectangle Bounds
        {
            get { return _bounds; }
        }

        public int X
        {
            get { return _bounds.X; }
            set { _bounds.X = value; }
        }

        public int Y
        {
            get { return _bounds.Y; }
            set { _bounds.Y = value; }
        }

        public Point Position
        {
            get { return _bounds.Location; }
            set { _bounds.Location = value; }
        }

        public GameObject(Texture2D image)
        {
            _image = image;
            _bounds = new Rectangle(0, 0, _image.Width, _image.Height);
        }

        public bool IsAttacking { get; private set; }

        public void Attack()
        {
            IsAttacking = true;
        }

        public void StopAttack()
        {
            IsAttacking = false;
        }

        public void CheckEnemyCollision(Enemy enemy)
        {
            if (IsAttacking && Bounds.Intersects(enemy.Bounds))
            {
                enemy.Remove(); // Chama um método para remover o inimigo da tela
                                // Aqui você pode adicionar a lógica para contar pontos
            }
        }

        public virtual void Initialize()
        {

        }

        public virtual void Update(float deltaTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_image, _bounds, Color.White);
        }

        // Método para calcular a direção do objeto em relação a outro objeto
        public Vector2 CalculateDirection(Vector2 targetPosition)
        {
            Vector2 currentPosition = new Vector2(_bounds.Center.X, _bounds.Center.Y);
            return Vector2.Normalize(targetPosition - currentPosition);
        }

        // Método para mover o objeto com uma determinada velocidade na direção especificada
        public void Move(Vector2 direction, float speed)
        {
            _bounds.X += (int)(direction.X * speed);
            _bounds.Y += (int)(direction.Y * speed);
        }
    }
}
