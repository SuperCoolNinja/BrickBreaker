using Experimenting2D.scenes;

namespace Experimenting2D.managers;


/// <summary>
/// This class represent the scene manager where we can add new scenes, loadscene, 
/// </summary>
internal class SceneManager
{
    private Dictionary<string, Scene> _scenes;
    private Scene? _currentScene = null;

    public SceneManager()
    {
        _scenes = new Dictionary<string, Scene>();
    }

    public void AddScene(string name, Scene scene)
    {
        _scenes.Add(name, scene);
    }

    public void LoadScene(string name)
    {
        if (_scenes.ContainsKey(name))
        {
            if (_currentScene is not null)
            {
                _currentScene.Unload();
            }

            _currentScene = _scenes[name];
            _currentScene.Load();
        }
    }

    public Scene? GetCurrentScene()
    {
        if (_currentScene is not null)
            return _currentScene;

        return null;
    }

    public void Update(float deltaTime)
    {
        if (_currentScene is not null)
        {
            _currentScene.Update(deltaTime);
        }
    }

    public void Draw()
    {
        if (_currentScene is not null)
        {
            _currentScene.Draw();
        }
    }

    public void Start()
    {
        if (_currentScene is not null)
        {
            _currentScene.Start();
        }
    }
}
