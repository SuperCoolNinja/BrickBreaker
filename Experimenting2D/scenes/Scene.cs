using Raylib_cs;

namespace Experimenting2D.scenes;

/// <summary>
/// Base class for scenes providing common functionality like load, unload, draw, update.
/// </summary>
public abstract class Scene
{
    protected bool Loaded { get; set; }

    public virtual void Load()
    {
        Loaded = true;
    }

    public virtual void Unload()
    {
        Loaded = false;
    }

    public virtual void Update(float deltaTime) { }
    public virtual void Draw() { }
    public virtual void Start() { }

    public virtual bool HasPlayerLost() => false;

    protected abstract List<(int X, int Y)> GenerateTargetPositions();
}
