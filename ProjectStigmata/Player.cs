using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectStigmata.Engine;
using System;
using static System.Net.Mime.MediaTypeNames;

public class Player : GameObject
{
    private const float SPEED_X = 200;
    private const float GRAVITY = 10;
    private Rectangle _previousBounds;
    private SpriteEffects _orientation;

    public Player(Texture2D image) : base(image)
    {
    }

    public override void Initialize()
    {
        _bounds.X = 150;
        _bounds.Y = 260;
        _orientation = SpriteEffects.None;
    }

    public override void Update(float deltaTime)
    {
        _previousBounds = _bounds;

        float direction = 0;
        if (Input.getKey(Keys.A) || Input.getKey(Keys.Left))
        {
            direction = -1;
            _orientation = SpriteEffects.FlipHorizontally;
        }
        else if (Input.getKey(Keys.D) || Input.getKey(Keys.Right))
        {
            direction = 1;
            _orientation = SpriteEffects.None;
        }

        if (direction != 0)
        {
            _bounds.X = _bounds.X + (int)(direction * SPEED_X * deltaTime);
        }

        // Verificar colisão com o chão (apenas para impedir que o jogador caia por enquanto)
        if (_bounds.Y >= 260) // Ajuste isso para a altura do chão
        {
            _bounds.Y = 260; // Ajuste isso para a altura do chão
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_image, _bounds, null, Color.White, 0, Vector2.Zero, _orientation, 0);
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
