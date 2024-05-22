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


    List<Particle> particles = new List<Particle>();
    Random random = new Random();

    private Vector2 _pos;

    public Paddle()
    {
        _screenWidth = Raylib.GetScreenWidth();
        _screenHeight = Raylib.GetScreenHeight();

        _width = 70;
        _height = 10;
        _pos.X = _screenWidth / 2 - _width / 2;
        _pos.Y = _screenHeight - _height * 2;
        _speed = 1000f;
    }


    public override void Draw()
    {
        Raylib.DrawRectangle((int)_pos.X, (int)_pos.Y, _width, _height, Color.RayWhite);

        foreach (var particle in particles)
            particle.Draw();
    }



    public override void Update(float deltaTime)
    {
        bool isMovingLeft = Raylib.IsKeyDown(KeyboardKey.Left) || Raylib.IsKeyDown(KeyboardKey.A) || Raylib.IsKeyDown(KeyboardKey.Q);
        bool isMovingRight = Raylib.IsKeyDown(KeyboardKey.Right) || Raylib.IsKeyDown(KeyboardKey.D);


        if (isMovingLeft && CanPaddleMoveLeft())
        {
            _pos.X -= 1 * _speed * deltaTime;
            CreateSmoke(_pos, particles, random);
        }

        if (isMovingRight && CanPaddleMoveRight())
        {
            _pos.X += 1 * _speed * deltaTime;
            CreateSmoke(_pos, particles, random);
        }

        foreach (var particle in particles)
            particle.Update();

        particles.RemoveAll(p => p.Alpha <= 0);
    }
    public Vector2 GetPos() => _pos;
    public int GetWidth() => _width;

    public int GetHeight() => _height;

    private bool CanPaddleMoveLeft() => _pos.X > 0;
    private bool CanPaddleMoveRight() => _pos.X < _screenWidth - _width;

    private void CreateSmoke(Vector2 paddlePosition, List<Particle> particles, Random random)
    {
        float size = (float)random.NextDouble() * 1 + 1;
        Vector2 position = new Vector2(paddlePosition.X + (float)random.NextDouble() * 1 - 5, paddlePosition.Y);
        Vector2 velocity = new Vector2((float)random.NextDouble() * 1 - 1, (float)random.NextDouble() * 6 - 3);
        particles.Add(new Particle(position, velocity, size));
    }
}
