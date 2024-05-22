using Experimenting2D.scenes;

namespace Experimenting2D.managers;


/// <summary>
/// This class represent the scene manager where we can add new scenes, loadscene, 
/// </summary>
internal class SceneManager
{
    private Dictionary<string, Scene> _scenes;
    private List<string> _sceneOrder;
    private int _currentSceneIndex;
    private Scene? _currentScene = null;

    public SceneManager()
    {
        _scenes = new Dictionary<string, Scene>();
        _sceneOrder = new List<string>();
        _currentSceneIndex = -1;
    }

    public void AddScene(string name, Scene scene)
    {
        _scenes.Add(name, scene);
        _sceneOrder.Add(name);
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
            _currentSceneIndex = _sceneOrder.IndexOf(name);
            _currentScene.Load();
        }
    }

    public void LoadNextScene()
    {
        if (_currentSceneIndex >= 0 && _currentSceneIndex < _sceneOrder.Count - 1)
        {
            LoadScene(_sceneOrder[_currentSceneIndex + 1]);
        }
    }

    public void ReloadCurrentScene()
    {
        if (_currentSceneIndex >= 0)
        {
            LoadScene(_sceneOrder[_currentSceneIndex]);
        }
    }

    public bool IsLastScene()
    {
        return _currentSceneIndex == _sceneOrder.Count - 1;
    }

    public Scene? GetCurrentScene()
    {
        return _currentScene;
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
