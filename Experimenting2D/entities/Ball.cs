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
            CheckCollisionWithPaddle();
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
                    _dir.Y *= -1;
                    break;
                }
            }
        }

        private void CheckCollisionWithPaddle()
        {
            if (_paddle == null)
                return;

            if (IsCollidingWithPaddle())
            {
                _dir.Y *= -1;
                float paddleCenter = _paddle.GetPos().X + _paddle.GetWidth() / 2;
                float hitPos = (_pos.X - paddleCenter) / (_paddle.GetWidth() / 2);
                _dir.X = hitPos;
            }
        }

        public bool IsBallOutOfZone()
        {
            return _pos.Y >= _screenHeight - RADIUS;
        }

        private bool IsCollidingWithPaddle()
        {
            return _pos.X + RADIUS > _paddle.GetPos().X && _pos.X - RADIUS < _paddle.GetPos().X + _paddle.GetWidth() &&
                   _pos.Y + RADIUS > _paddle.GetPos().Y && _pos.Y - RADIUS < _paddle.GetPos().Y + _paddle.GetWidth();
        }


        // Thanks to gpt for that collision with the target pos check :
        private bool IsCollidingWithTarget((int X, int Y) targetPos)
        {
            return _pos.X + RADIUS > targetPos.X && _pos.X - RADIUS < targetPos.X + _targetWidth &&
                   _pos.Y + RADIUS > targetPos.Y && _pos.Y - RADIUS < targetPos.Y + _targetHeight;
        }
    }
}
