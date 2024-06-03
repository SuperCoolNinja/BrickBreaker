using Experimenting2D.config;
using Experimenting2D.entities.@base;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Experimenting2D.entities
{
    /// <summary>
    /// Represents a paddle entity in the game.
    /// </summary>
    internal class Paddle : Entity
    {
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

            _pos.X = _screenWidth / 2 - GameConfig.PaddleTexture.Width / 2;
            _pos.Y = _screenHeight - GameConfig.PaddleTexture.Height;

            Console.WriteLine("HEIGHT" + GameConfig.PaddleTexture.Height);
            _speed = 1000f;
        }

        public override void Draw()
        {
            Raylib.DrawTexture(GameConfig.PaddleTexture, (int)_pos.X, (int)_pos.Y, Color.White);

            // Draw particles
            foreach (var particle in particles)
                particle.Draw();
        }

        public override void Update(float deltaTime)
        {
            bool isMovingLeft = Raylib.IsKeyDown(KeyboardKey.Left) || Raylib.IsKeyDown(KeyboardKey.A) || Raylib.IsKeyDown(KeyboardKey.Q);
            bool isMovingRight = Raylib.IsKeyDown(KeyboardKey.D) || Raylib.IsKeyDown(KeyboardKey.Right);

            if (isMovingLeft && CanPaddleMoveLeft())
            {
                _pos.X -= _speed * deltaTime;
                CreateSmoke(_pos, particles, random);
            }

            if (isMovingRight && CanPaddleMoveRight())
            {
                _pos.X += _speed * deltaTime;
                CreateSmoke(_pos, particles, random);
            }

            if (_pos.X < 0)
                _pos.X = 0;

            if (_pos.X > _screenWidth - GameConfig.PaddleTexture.Width)
                _pos.X = _screenWidth - GameConfig.PaddleTexture.Width;

            foreach (var particle in particles)
                particle.Update();

            particles.RemoveAll(p => p.Alpha <= 0);
        }

        public Vector2 GetPos() => _pos;
        public int GetWidth() => GameConfig.PaddleTexture.Width;
        public int GetHeight() => GameConfig.PaddleTexture.Height;

        private bool CanPaddleMoveLeft() => _pos.X > 0;
        private bool CanPaddleMoveRight() => _pos.X < _screenWidth - GameConfig.PaddleTexture.Width;

        private void CreateSmoke(Vector2 paddlePosition, List<Particle> particles, Random random)
        {
            float size = (float)random.NextDouble() * 1 + 1;
            Vector2 position = new Vector2(paddlePosition.X + (float)random.NextDouble() * 1 - 5, paddlePosition.Y);
            Vector2 velocity = new Vector2((float)random.NextDouble() * 1 - 1, (float)random.NextDouble() * 6 - 3);
            particles.Add(new Particle(position, velocity, size));
        }
    }
}
