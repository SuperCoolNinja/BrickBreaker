using Experimenting2D.entities.@base;
using Raylib_cs;
using System.Numerics;

namespace Experimenting2D.entities;


/// <summary>
/// Represents a paddle entity in the game.
/// </summary>
internal class Paddle : Entity
{
    private int _width;
    private int _height;

    private float _speed;

    private Vector2 _pos;

    public Paddle()
    {
        _width = 60;
        _height = 10;
        _pos.X = Raylib.GetScreenWidth() / 2 - _width / 2;
        _pos.Y = Raylib.GetScreenHeight() - _height * 2;
        _speed = 1200f;
    }


    public override void Draw()
    {
        Raylib.DrawRectangle((int)_pos.X, (int)_pos.Y, _width, _height, Color.White);
    }

    public override void Update(float deltaTime)
    {
        bool isMovingLeft = Raylib.IsKeyDown(KeyboardKey.Left) || Raylib.IsKeyDown(KeyboardKey.A) || Raylib.IsKeyDown(KeyboardKey.Q);
        bool isMovingRight = Raylib.IsKeyDown(KeyboardKey.Right) || Raylib.IsKeyDown(KeyboardKey.D);


        if (isMovingLeft && CanPaddleMoveLeft())
            _pos.X -= 1 * _speed * deltaTime;

        if (isMovingRight && CanPaddleMoveRight())
            _pos.X += 1 * _speed * deltaTime;
    }

    private bool CanPaddleMoveLeft() => _pos.X > 0;
    private bool CanPaddleMoveRight() => _pos.X < Raylib.GetScreenWidth() - _width;
}
