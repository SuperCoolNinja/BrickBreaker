using Experimenting2D.config;
using Experimenting2D.managers;
using Experimenting2D.scenes;
using Raylib_cs;

namespace Experimenting2D;

internal class Game
{
    private SceneManager _sceneManager;
    private bool _endGame = false;


    public void Run()
    {
        Raylib.InitWindow(GameConfig.ScreenWidth, GameConfig.ScreenHeight, GameConfig.Title);

        // Optionally, set a target FPS, but VSync will typically override this
        Raylib.SetTargetFPS(GameConfig.TargetFPS);

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
        float deltaTime = Raylib.GetFrameTime();

        if (_endGame)
        {
            Raylib.DrawText("PRESS R TO RESTART", Raylib.GetScreenWidth() / 2 - 100, Raylib.GetScreenHeight() / 2 - 12, 24, Color.Red);

            if (Raylib.IsKeyPressed(KeyboardKey.R))
            {
                _sceneManager.LoadScene("first level");
                _sceneManager.Start();
                _endGame = false;
            }
        }
        else
        {
            _sceneManager.Update(deltaTime);
            _endGame = CheckPlayerLost();
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
}
