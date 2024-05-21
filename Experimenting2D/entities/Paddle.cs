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

    private int _screenWidth;
    private int _screenHeight;


    private Vector2 _pos;

    public Paddle()
    {
        _screenWidth = Raylib.GetScreenWidth();
        _screenHeight = Raylib.GetScreenHeight();

        _width = 70;
        _height = 10;
        _pos.X = _screenWidth / 2 - _width / 2;
        _pos.Y = _screenHeight - _height * 2;
        _speed = 1400f;
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
    public Vector2 GetPos() => _pos;
    public int GetWidth() => _width;

    private bool CanPaddleMoveLeft() => _pos.X > 13;
    private bool CanPaddleMoveRight() => _pos.X < _screenWidth - _width * 1.39;
}
