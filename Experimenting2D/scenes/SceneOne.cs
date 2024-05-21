using Experimenting2D.entities;
using Experimenting2D.scenes;
using Raylib_cs;

internal class SceneOne : Scene
{
    private Paddle _paddle;
    private Target _target;
    private Ball _ball;

    private const int ROWS = 8;
    private const int COLS = 8;

    private const int WIDTH = 80;
    private const int HEIGHT = 15;

    public override void Load()
    {
        base.Load();

        _paddle = new Paddle();
        _ball = new Ball(_paddle);
        _target = new Target(WIDTH, HEIGHT);
    }

    public override void Unload()
    {
        base.Unload();
    }

    public override void Start()
    {
        base.Start();

        _target.Start();

        var targetPositions = GenerateTargetPositions();

        _target.SetPositions(targetPositions);
        _ball.SetTargetPositions(targetPositions, WIDTH, HEIGHT);
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        _paddle.Update(deltaTime);
        _target.Update(deltaTime);
        _ball.Update(deltaTime);
    }

    public override bool HasPlayerLost()
    {
        return _ball.IsBallOutOfZone();
    }

    public override void Draw()
    {
        base.Draw();

        _paddle.Draw();
        _target.Draw();
        _ball.Draw();
    }

    protected override List<(int X, int Y)> GenerateTargetPositions()
    {
        var positions = new List<(int X, int Y)>();

        int screenWidth = Raylib.GetScreenWidth();

        int totalGridWidth = COLS * WIDTH + (COLS - 1) * 2;

        int offsetX = (screenWidth - totalGridWidth) / 2;
        int offsetY = 2;

        for (int r = 0; r < ROWS; r++)
        {
            for (int c = 0; c < COLS; c++)
            {
                int posX = offsetX + c * (WIDTH + 2);
                int posY = offsetY + r * (HEIGHT + 2);
                positions.Add((posX, posY));
            }
        }

        return positions;
    }
}
