using Raylib_cs;

namespace Experimenting2D.config;

public static class GameConfig
{
    public const int ScreenWidth = 800;
    public const int ScreenHeight = 600;
    public const int TargetFPS = 60;
    public const string Title = "Brick Breaker Game by SuperCoolNinja using Raylib.";
    public const string PADDLE_TEXTURE = "resources/gray_brick.png";
    public static Texture2D PaddleTexture { get; set; }

    public static void SetTexturePaddle(Texture2D paddleTexture)
    {
        PaddleTexture = paddleTexture;
    }
}
