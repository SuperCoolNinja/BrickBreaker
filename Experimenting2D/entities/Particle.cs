using Raylib_cs;
using System.Numerics;

namespace Experimenting2D.entities
{
    internal class Particle
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public float Alpha;
        public float Size;

        public Particle(Vector2 position, Vector2 velocity, float size)
        {
            Position = position;
            Velocity = velocity;
            Size = size;
            Alpha = 1.0f;
        }

        public void Update()
        {
            Position += Velocity;
            Alpha -= 0.01f;
            if (Alpha < 0) Alpha = 0;
        }

        public void Draw()
        {
            Color color = new Color(255, 255, 255, (int)(Alpha * 255));
            Raylib.DrawCircleV(Position, Size, color);
        }
    }
}
