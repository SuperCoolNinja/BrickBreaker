﻿using Experimenting2D.config;
using Experimenting2D.entities;
using Experimenting2D.managers;
using Experimenting2D.scenes;
using Raylib_cs;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace Experimenting2D
{
    internal class Game
    {
        private SceneManager _sceneManager;
        private bool _playerLoose = false;
        private bool _canGoNextLevel = false;
        private bool _isPaused = false;
        private Font _boldFont;

        public void Run()
        {
            Raylib.InitWindow(GameConfig.ScreenWidth, GameConfig.ScreenHeight, GameConfig.Title);
            Raylib.SetTargetFPS(GameConfig.TargetFPS);

            // Load a custom bold font
            _boldFont = Raylib.LoadFont("resources/fonts/bold.ttf");

            // Charger l'image du paddle
            Image paddleImage = Raylib.LoadImage(GameConfig.PADDLE_TEXTURE);
            // Redimensionner l'image du paddle
            Raylib.ImageResize(ref paddleImage, 85, 20);
            // Charger la texture du paddle à partir de l'image
            GameConfig.SetTexturePaddle(Raylib.LoadTextureFromImage(paddleImage));
            // Décharger l'image du paddle
            Raylib.UnloadImage(paddleImage);

            _sceneManager = new SceneManager();
            _sceneManager.AddScene("first level", new SceneOne());
            _sceneManager.AddScene("second level", new SceneTwo());

            _sceneManager.LoadScene("first level");



            _sceneManager.Start();

            while (!Raylib.WindowShouldClose())
            {
                if (!Raylib.IsWindowFocused())
                {
                    _isPaused = true;
                }

                Update();
                Draw();
            }

            Raylib.UnloadTexture(GameConfig.PaddleTexture);
            Raylib.UnloadFont(_boldFont);
            Raylib.CloseWindow();
        }

        private void Initialize()
        {
            Raylib.InitWindow(GameConfig.ScreenWidth, GameConfig.ScreenHeight, GameConfig.Title);
            Raylib.SetTargetFPS(GameConfig.TargetFPS);

            _boldFont = Raylib.LoadFont("resources/fonts/bold.ttf");

            _sceneManager = new SceneManager();
            _sceneManager.AddScene("first level", new SceneOne());
            _sceneManager.AddScene("second level", new SceneTwo());
            _sceneManager.LoadScene("first level");

            LoadPaddleTexture();
            _sceneManager.Start();
        }

        private void LoadPaddleTexture()
        {
            Image image = Raylib.LoadImage(GameConfig.PADDLE_TEXTURE);
            Raylib.ImageResize(ref image, 80, 20);
            GameConfig.PaddleTexture = Raylib.LoadTextureFromImage(image);
            Raylib.UnloadImage(image);
        }

        private void Update()
        {
            if (_isPaused)
            {
                if (Raylib.IsWindowFocused())
                {
                    _isPaused = false;
                }
                return;
            }

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
                _canGoNextLevel = CanLoadNextLevel();
            }
        }

        private void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(new Color(91, 120, 228, 255));

            _sceneManager.Draw();

            if (_playerLoose)
                DrawRestartText();

            if (_isPaused)
                DrawPausedText();

            Raylib.EndDrawing();
        }

        private void DrawRestartText()
        {
            string message = "PRESS R TO RESTART";
            float fontSize = 24;
            float textWidth = Raylib.MeasureTextEx(_boldFont, message, fontSize, 1f).X;

            int screenWidth = Raylib.GetScreenWidth();
            int screenHeight = Raylib.GetScreenHeight();
            int posX = screenWidth / 2 - (int)textWidth / 2;
            int posY = screenHeight / 2 - (int)fontSize / 2;

            int padding = 10;
            Raylib.DrawRectangle(posX - padding, posY - padding, (int)textWidth + padding * 2, (int)fontSize + padding * 2, new Color(0, 0, 0, 200));
            Raylib.DrawTextEx(_boldFont, message, new Vector2(posX, posY), fontSize, 1, Color.Red);
        }

        private void DrawPausedText()
        {
            string message = "PAUSED";
            float fontSize = 24;
            float textWidth = Raylib.MeasureTextEx(_boldFont, message, fontSize, 1f).X;

            int screenWidth = Raylib.GetScreenWidth();
            int screenHeight = Raylib.GetScreenHeight();
            int posX = screenWidth / 2 - (int)textWidth / 2;
            int posY = screenHeight / 2 - (int)fontSize / 2;

            int padding = 10;
            Raylib.DrawRectangle(posX - padding, posY - padding, (int)textWidth + padding * 2, (int)fontSize + padding * 2, new Color(0, 0, 0, 200));
            Raylib.DrawTextEx(_boldFont, message, new Vector2(posX, posY), fontSize, 1, Color.Red);
        }

        private bool CheckPlayerLost()
        {
            Scene currentScene = _sceneManager.GetCurrentScene();
            return currentScene != null && currentScene.HasPlayerLost();
        }

        private bool CanLoadNextLevel()
        {
            Scene currentScene = _sceneManager.GetCurrentScene();
            return currentScene != null && currentScene.HasDestroyAllTarget();
        }

        private void UnloadAssets()
        {
            Raylib.UnloadTexture(GameConfig.PaddleTexture);
            Raylib.UnloadFont(_boldFont);
        }
    }
}
