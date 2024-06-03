using Experimenting2D.entities.@base;
using Raylib_cs;
using System.Numerics;

namespace Experimenting2D.entities
{
    internal class Ball : Entity
    {
        private const int RADIUS = 5;
        private Vector2 _pos;
        private Vector2 _dir;

        private int _screenWidth;
        private int _screenHeight;

        private List<(int X, int Y)> _targetPositions;
        private int _targetWidth;
        private int _targetHeight;

        const float SPEED = 350f;

        List<Particle> particles = new List<Particle>();
        Random random = new Random();

        private Paddle _paddle;

        public Ball(Paddle paddle)
        {
            _screenWidth = Raylib.GetScreenWidth();
            _screenHeight = Raylib.GetScreenHeight();

            _pos.X = _screenWidth / 2 - RADIUS / 2;
            _pos.Y = _screenHeight - RADIUS * 7;

            _dir.X = 0;
            _dir.Y = -1;

            _paddle = paddle;
        }


        public override void Draw()
        {
            base.Draw();

            Raylib.DrawCircle((int)_pos.X, (int)_pos.Y, RADIUS, Color.White);

            foreach (var particle in particles)
                particle.Draw();
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            float nextPosX = _pos.X + _dir.X * SPEED * deltaTime;
            float nextPosY = _pos.Y + _dir.Y * SPEED * deltaTime;

            if (nextPosX <= 5 || nextPosX >= _screenWidth - RADIUS)
                _dir.X *= -1;

            if (nextPosY <= 0)
                _dir.Y *= -1;

            _pos.X = nextPosX;
            _pos.Y = nextPosY;

            CheckCollisionWithTargets();

            foreach (var particle in particles)
                particle.Update();

            particles.RemoveAll(p => p.Alpha <= 0);
        }

        public void SetTargetPositions(List<(int X, int Y)> targetPositions, int targetWidth, int targetHeight)
        {
            _targetPositions = targetPositions;
            _targetWidth = targetWidth;
            _targetHeight = targetHeight;
        }

        private void CheckCollisionWithTargets()
        {
            if (_targetPositions == null)
                return;

            for (int i = 0; i < _targetPositions.Count; i++)
            {
                var targetPos = _targetPositions[i];
                if (IsCollidingWithTarget(targetPos))
                {
                    _targetPositions.RemoveAt(i);
                    //_dir.Y *= -1; // collision on target bounce good for destroyable target.
                    break;
                }
            }
        }

        public bool IsBallOutOfZone()
        {
            return _pos.Y > _screenHeight - RADIUS;
        }

        public bool IsCollidingWithPaddle()
        {
            return _pos.X + RADIUS > _paddle.GetPos().X && _pos.X - RADIUS < _paddle.GetPos().X + _paddle.GetWidth() &&
                   _pos.Y + RADIUS > _paddle.GetPos().Y && _pos.Y - RADIUS < _paddle.GetPos().Y + _paddle.GetWidth();
        }

        public void BounceOffPaddle(Paddle paddle)
        {
            _dir.Y *= -1;
            float paddleCenter = _paddle.GetPos().X + _paddle.GetWidth() / 2;
            float hitPos = (_pos.X - paddleCenter) / (_paddle.GetWidth() / 2);
            _dir.X = hitPos;
            CreateSmoke(_pos, particles, random);
        }

        private void CreateSmoke(Vector2 paddlePosition, List<Particle> particles, Random random)
        {
            for (int i = 0; i < 10; i++)
            {
                float size = (float)random.NextDouble() * 3 + 2;
                Vector2 position = new Vector2(paddlePosition.X, paddlePosition.Y + _paddle.GetHeight() / 2);
                float angle = (float)random.NextDouble() * MathF.PI * 2;
                float speed = (float)random.NextDouble() * 2 + 3;
                Vector2 velocity = new Vector2(MathF.Cos(angle) * speed, MathF.Sin(angle) / 2 * speed);
                particles.Add(new Particle(position, velocity, size));
            }
        }

        // Thanks to gpt for that collision with the target pos check :
        private bool IsCollidingWithTarget((int X, int Y) targetPos)
        {
            return _pos.X + RADIUS > targetPos.X && _pos.X - RADIUS < targetPos.X + _targetWidth &&
                   _pos.Y + RADIUS > targetPos.Y && _pos.Y - RADIUS < targetPos.Y + _targetHeight;
        }
    }
}
