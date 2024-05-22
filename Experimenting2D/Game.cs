using Experimenting2D.config;
using Experimenting2D.managers;
using Experimenting2D.scenes;
using Raylib_cs;

namespace Experimenting2D;

internal class Game
{
    private SceneManager _sceneManager;
    private bool _playerLoose = false;
    private bool _canGoNextLevel = false;

    public void Run()
    {
        Raylib.InitWindow(GameConfig.ScreenWidth, GameConfig.ScreenHeight, GameConfig.Title);

        // Optionally, set a target FPS, but VSync will typically override this
        Raylib.SetTargetFPS(GameConfig.TargetFPS);

        _sceneManager = new SceneManager();
        _sceneManager.AddScene("first level", new SceneOne());
        _sceneManager.AddScene("second level", new SceneTwo());

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
        float deltaTime = Raylib.GetFrameTime();

        if (_canGoNextLevel)
        {
            if (!_sceneManager.IsLastScene())
            {
                _sceneManager.LoadNextScene();
                _sceneManager.Start();
            }
            _canGoNextLevel = false;
        }

        if (_playerLoose)
        {
            Raylib.DrawText("PRESS R TO RESTART", Raylib.GetScreenWidth() / 2 - 100, Raylib.GetScreenHeight() / 2 - 12, 24, Color.Red);

            if (Raylib.IsKeyPressed(KeyboardKey.R))
            {
                _sceneManager.ReloadCurrentScene();
                _sceneManager.Start();
                _playerLoose = false;
            }
        }
        else
        {
            _sceneManager.Update(deltaTime);
            _playerLoose = CheckPlayerLost();
            _canGoNextLevel = CheckPlayerWon();
        }
    }

    private void Draw()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.Black);

        _sceneManager.Draw();

        Raylib.EndDrawing();
    }

    private bool CheckPlayerLost()
    {
        if (_sceneManager.GetCurrentScene() is null)
            return false;

        Scene scene = _sceneManager.GetCurrentScene();
        if (scene != null)
            return scene.HasPlayerLost();

        return false;
    }

    private bool CheckPlayerWon()
    {
        Scene? scene = _sceneManager.GetCurrentScene();

        if (scene != null)
        {
            return scene.HasDestroyAllTarget();
        }

        return false;
    }
}
