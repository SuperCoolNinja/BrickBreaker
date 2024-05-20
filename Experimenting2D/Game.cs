using Experimenting2D.config;
using Experimenting2D.managers;
using Experimenting2D.scenes;
using Raylib_cs;
using System.Diagnostics;

namespace Experimenting2D;

internal class Game
{
    private Stopwatch _stopwatch;
    private float _deltaTime;
    private float _timeSinceLastUpdate;
    private float _updateInterval;

    private SceneManager _sceneManager;

    public void Run()
    {
        Raylib.InitWindow(GameConfig.ScreenWidth, GameConfig.ScreenHeight, GameConfig.Title);
        Raylib.SetTargetFPS(GameConfig.TargetFPS);

        _stopwatch = new Stopwatch();
        _stopwatch.Start();
        _updateInterval = 1.0f / GameConfig.TargetFPS;


        _sceneManager = new SceneManager();
        _sceneManager.AddScene("first level", new SceneOne());
        _sceneManager.LoadScene("first level");

        _sceneManager.Start();

        while (!Raylib.WindowShouldClose())
        {
            Update();
            Draw();
        }

        Raylib.CloseWindow();
    }

    private void Update()
    {
        float elapsedTime = (float)_stopwatch.Elapsed.TotalSeconds;
        _stopwatch.Restart();
        _deltaTime = elapsedTime;

        _timeSinceLastUpdate += _deltaTime;

        if (_timeSinceLastUpdate >= _updateInterval)
        {
            _sceneManager.Update(_timeSinceLastUpdate);
            _timeSinceLastUpdate = 0.0f;
        }
    }

    private void Draw()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.Black);

        _sceneManager.Draw();

        Raylib.EndDrawing();
    }
}
