using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectStigmata.Engine;
using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

public class Player : GameObject
{
    private const float SPEED_X = 200;
    private const float GRAVITY = 10;
    private Rectangle _previousBounds;
    private SpriteEffects _orientation;

    private List<Texture2D> _animationFrames;
    private int _currentFrameIndex;
    private float _frameTime;
    private const float FRAME_DURATION = 0.1f;

    public Player(Texture2D image) : base(image)
    {
        _animationFrames = new List<Texture2D>();
        _currentFrameIndex = 0;
        _frameTime = 0f;
    }

    // Método para carregar as texturas da animação
    public void LoadAnimationFrames(List<Texture2D> frames)
    {
        _animationFrames.AddRange(frames);
    }

    public override void Initialize()
    {
        _bounds.X = 150;
        _bounds.Y = 260;
        _orientation = SpriteEffects.None;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        _previousBounds = _bounds;

        // Verificar movimento e atualizar a animação
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

        // Atualizar a posição do jogador
        if (direction != 0)
        {
            _bounds.X += (int)(direction * SPEED_X * deltaTime);
            Animate(deltaTime);
        }
        else
        {
            // Se não estiver se movendo, exibir o frame de TristanIdle
            _currentFrameIndex = 0;
        }

        // Verificar colisão com o chão 
        if (_bounds.Y >= 260) // Altura do chão
        {
            _bounds.Y = 260; 
        }
    }

    // Método para atualizar a animação com base no tempo
    private void Animate(float deltaTime)
    {
        _frameTime += deltaTime;

        // Defina o índice inicial para o primeiro frame de corrida
        int startIndex = 1;
        // Defina o índice final para o último frame de corrida
        int endIndex = 6;

        // Determine o número total de frames de corrida
        int totalFrames = endIndex - startIndex + 1;

        // Calcule o tempo total necessário para percorrer todos os frames de corrida uma vez
        float totalAnimationTime = totalFrames * FRAME_DURATION;

        // Aplique o loop na animação de corrida
        if (_frameTime >= totalAnimationTime)
        {
            // Recomece a animação da corrida
            _frameTime = 0f;
        }

        // Determine o índice do frame com base no tempo atual
        _currentFrameIndex = startIndex + (int)(_frameTime / FRAME_DURATION) % totalFrames;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_animationFrames[_currentFrameIndex], _bounds, null, Color.White, 0, Vector2.Zero, _orientation, 0);
    }

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
