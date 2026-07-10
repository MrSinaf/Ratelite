namespace Ratelite;

public class Scene
{
	private readonly List<IPlugin> plugins = [];
	public readonly ComponentSystem components = new();
	
	public static event Action onSceneInitialized = delegate { };
	
	public bool isRunning { get; private set; }
	
	public virtual void Init() { }
	public virtual void Start() { }
	public virtual void Update() { }
	public virtual void Render() { }
	
	public virtual Task Load() => Task.CompletedTask;
	public virtual void Unload() { }
	
	public T AddPlugin<T>() where T : class, IPlugin, new()
	{
		foreach (var plugin in plugins)
			if (plugin.GetType() == typeof(T))
				throw new Exception($"Plugin {typeof(T).Name} already added to scene");
		
		var instance = new T();
		plugins.Add(instance);
		return instance;
	}
	
	public T GetPlugin<T>() where T : class, IPlugin
	{
		foreach (var plugin in plugins)
			if (plugin is T instance)
				return instance;
		
		throw new Exception($"Plugin {typeof(T).Name} not found in scene");
	}
	
	public bool TryGetPlugin<T>(out T? plugin) where T : class, IPlugin
	{
		foreach (var mPlugin in plugins)
			if (mPlugin is T m)
			{
				plugin = m;
				return true;
			}
		
		plugin = null;
		return false;
	}
	
	internal void InternalUpdate()
	{
		if (!isRunning)
		{
			InternalStart();
			return;
		}
		
		foreach (var plugin in plugins)
		{
			try
			{
				plugin.Update();
			}
			catch (Exception e)
			{
				Log.Write($"Plugin error: ({plugin.GetType()})\n" + e.Message, Log.Level.Error);
			}
		}
		components.Update();
		Update();
	}
	
	internal void InternalRender()
	{
		foreach (var plugin in plugins)
		{
			try
			{
				plugin.Render();
			}
			catch (Exception e)
			{
				Log.Write(
					$"Plugin error: ({plugin.GetType()})\n" + e.Message +
					(e.StackTrace != null ? $"\n{e.StackTrace}" : string.Empty),
					Log.Level.Error
				);
			}
		}
		components.Render();
		Render();
	}
	
	private void InternalStart()
	{
		Init();
		foreach (var plugin in plugins)
		{
			try
			{
				plugin.Init();
			}
			catch (Exception e)
			{
				Log.Write($"Plugin error: ({plugin.GetType()})\n" + e.Message, Log.Level.Error);
			}
		}
		Start();
		isRunning = true;
		onSceneInitialized();
	}
	
	internal void InternalUnload()
	{
		Unload();
		foreach (var plugin in plugins)
		{
			try
			{
				plugin.Destroy();
			}
			catch (Exception e)
			{
				Log.Write($"Plugin error: ({plugin.GetType()})\n" + e.Message, Log.Level.Error);
			}
		}
		
		components.Destroy();
	}
}