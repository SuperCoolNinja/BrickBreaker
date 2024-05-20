using Experimenting2D.entities.@base;
using Raylib_cs;

namespace Experimenting2D.entities;

internal class Target : Entity
{
    private int _width;
    private int _height;

    private List<(int X, int Y)> _positions;

    public Target(int width, int height)
    {
        _width = width;
        _height = height;
        _positions = new List<(int X, int Y)>();
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Draw()
    {
        base.Draw();

        foreach (var position in _positions)
        {
            Raylib.DrawRectangle(position.X, position.Y, _width, _height, Color.Blue);
        }
    }
    public void SetPositions(List<(int X, int Y)> positions)
    {
        _positions = positions;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
    }
}
