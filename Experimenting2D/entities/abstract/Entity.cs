namespace Experimenting2D.entities.@base;


/// <summary>
/// Base class for game entities providing common functionality like drawing and updating.
/// </summary>
public abstract class Entity
{
    public virtual void Draw() { }
    public virtual void Update(float deltaTime) { }
    public virtual void Start() { }
}
