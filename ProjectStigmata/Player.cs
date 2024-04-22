using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectStigmata.Engine;
using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

public class Player : GameObject
{
    private const float SPEED_X = 200; // Velocidade horizontal do jogador
    private const float GRAVITY = 10; // Gravidade aplicada ao jogador
    private Rectangle _previousBounds; // Retângulo representando as dimensões anteriores do jogador
    private SpriteEffects _orientation; // Orientação do jogador (esquerda ou direita)
    public new Vector2 Position => new Vector2(_bounds.X, _bounds.Y); // Propriedade para acessar a posição do jogador

    // Variáveis para animação de corrida
    private List<Texture2D> _animationFrames; // Lista de texturas para a animação de corrida
    private int _currentFrameIndex; // Índice do frame atual da animação de corrida
    private float _frameTime; // Tempo decorrido desde o último frame
    private const float FRAME_DURATION = 0.1f; // Duração de cada frame de animação

    // Variáveis para animação de ataque
    private bool _isAttacking; // Flag indicando se o jogador está atacando
    private List<Texture2D> _attackFrames; // Lista de texturas para a animação de ataque
    private const float ATTACK_FRAME_DURATION = 0.1f; // Duração de cada frame de animação de ataque
    private float _attackFrameTime; // Tempo decorrido desde o último frame de ataque
    private int _currentAttackFrameIndex; // Índice do frame atual da animação de ataque
    private bool _wasAttackButtonPressed = false; // Flag indicando se o botão de ataque foi pressionado recentemente

    // Construtor da classe Player
    public Player(Texture2D image) : base(image)
    {
        _animationFrames = new List<Texture2D>(); // Inicializa a lista de texturas de animação de corrida
        _currentFrameIndex = 0; // Inicializa o índice do frame atual da animação de corrida
        _frameTime = 0f; // Inicializa o tempo decorrido desde o último frame

        _isAttacking = false; // Inicializa a flag de ataque como falsa
        _attackFrames = new List<Texture2D>(); // Inicializa a lista de texturas de animação de ataque
        _attackFrameTime = 0f; // Inicializa o tempo decorrido desde o último frame de ataque
        _currentAttackFrameIndex = 0; // Inicializa o índice do frame atual da animação de ataque
    }

    // Método de inicialização do jogador
    public override void Initialize()
    {
        _bounds.X = 400; // Define a posição inicial horizontal do jogador
        _bounds.Y = 260; // Define a posição inicial vertical do jogador
        _orientation = SpriteEffects.None; // Define a orientação inicial do jogador
    }

    // Método de atualização do jogador
    public override void Update(float deltaTime)
    {
        base.Update(deltaTime); // Chama o método de atualização da classe base (GameObject)

        _previousBounds = _bounds; // Armazena as dimensões anteriores do jogador

        // Verifica o movimento horizontal do jogador e atualiza a animação
        float direction = 0;
        if (Input.getKey(Keys.A) || Input.getKey(Keys.Left))
        {
            direction = -1;
            _orientation = SpriteEffects.FlipHorizontally;
        }
        if (Input.getKey(Keys.D) || Input.getKey(Keys.Right))
        {
            direction = 1;
            _orientation = SpriteEffects.None;
        }

        // Verifica se o jogador está tentando atacar
        if (Input.getKey(Keys.X) && !_isAttacking && !_wasAttackButtonPressed)
        {
            _wasAttackButtonPressed = true; // Marca o botão de ataque como pressionado
            _isAttacking = true; // Inicia a animação de ataque
            _currentAttackFrameIndex = 0; // Reinicia o índice do frame de ataque
            _attackFrameTime = 0f; // Reinicia o tempo decorrido desde o último frame de ataque
        }
        else if (!Input.getKey(Keys.X)) // Se o botão de ataque foi liberado
        {
            _wasAttackButtonPressed = false; // Marca o botão de ataque como liberado
        }

        // Atualiza a posição horizontal do jogador e a animação de corrida
        if (direction != 0)
        {
            _bounds.X += (int)(direction * SPEED_X * deltaTime);
            Animate(deltaTime);
        }
        else
        {
            // Se o jogador não estiver se movendo, exibe o frame de repouso
            _currentFrameIndex = 0;
        }

        // Atualiza a animação de ataque
        if (_isAttacking)
        {
            _attackFrameTime += deltaTime;

            if (_attackFrameTime >= ATTACK_FRAME_DURATION)
            {
                _attackFrameTime = 0f;
                _currentAttackFrameIndex++;

                // Verifica se chegamos ao último frame de ataque
                if (_currentAttackFrameIndex >= _attackFrames.Count)
                {
                    _currentAttackFrameIndex = 0;
                    _isAttacking = false; // Finaliza a animação de ataque
                }
            }
        }

        // Verifica e ajusta a colisão com o chão 
        if (_bounds.Y >= 260) // Altura do chão
        {
            _bounds.Y = 260; // Mantém o jogador sobre o chão
        }
    }

    // Método para atualizar a animação de corrida com base no tempo
    private void Animate(float deltaTime)
    {
        _frameTime += deltaTime;

        // Determina os índices inicial e final da animação de corrida
        int startIndex = 1;
        int endIndex = 6;

        // Calcula o número total de frames de corrida
        int totalFrames = endIndex - startIndex + 1;

        // Calcula o tempo total necessário para percorrer todos os frames de corrida uma vez
        float totalAnimationTime = totalFrames * FRAME_DURATION;

        // Aplica o loop na animação de corrida
        if (_frameTime >= totalAnimationTime)
        {
            // Reinicia a animação de corrida
            _frameTime = 0f;
        }

        // Determina o índice do frame com base no tempo atual
        _currentFrameIndex = startIndex + (int)(_frameTime / FRAME_DURATION) % totalFrames;
    }

    // Método para carregar as texturas da animação de corrida
    public void LoadAnimationFrames(List<Texture2D> frames)
    {
        _animationFrames.AddRange(frames);
    }

    // Método para carregar as texturas da animação de ataque
    public void LoadAttackFrames(List<Texture2D> frames)
    {
        _attackFrames.AddRange(frames);
    }

    // Método para desenhar o jogador na tela
    public override void Draw(SpriteBatch spriteBatch)
    {
        // Desenha a animação de ataque se o jogador estiver atacando
        if (_isAttacking)
        {
            spriteBatch.Draw(_attackFrames[_currentAttackFrameIndex], _bounds, null, Color.White, 0, Vector2.Zero, _orientation, 0);
        }
        else // Se não estiver atacando, desenha a animação de corrida ou repouso
        {
            spriteBatch.Draw(_animationFrames[_currentFrameIndex], _bounds, null, Color.White, 0, Vector2.Zero, _orientation, 0);
        }
    }

    public bool CheckCollision(Enemy enemy)
    {
        // Verifica se há interseção entre os retângulos delimitadores do jogador e do inimigo
        if (_bounds.Intersects(enemy.Bounds))
        {
            // Se houver interseção, retorna true (houve colisão)
            return true;
        }

        // Se não houver interseção, retorna false (não houve colisão)
        return false;
    }

    // Método para verificar e resolver colisões com outros objetos do jogo
    public void CheckBlockers(GameObject[] gameObjects)
    {
        foreach (GameObject item in gameObjects)
        {
            Rectangle intersection = Rectangle.Intersect(_bounds, item.Bounds);
            if (intersection.Width > 0)
            {
                if ((_previousBounds.Right <= item.X) ||
                    (_previousBounds.X >= item.Bounds.Right))
                {
                    int sign = Math.Sign(_previousBounds.X - _bounds.X);
                    if (sign < 0)
                    {
                        _bounds.X = item.X - _bounds.Width;
                    }
                    else
                    {
                        _bounds.X = item.X + item.Bounds.Width;
                    }
                }
            }
            if (intersection.Height > 0)
            {
                if ((_previousBounds.Bottom <= item.Y) ||
                    (_previousBounds.Y >= item.Bounds.Bottom))
                {
                    int sign = Math.Sign(_previousBounds.Y - _bounds.Y);
                    if (sign < 0)
                    {
                        _bounds.Y = item.Y - _bounds.Height;
                    }
                    else
                    {
                        _bounds.Y = item.Y + item.Bounds.Height;
                    }
                }
            }
        }
    }
}