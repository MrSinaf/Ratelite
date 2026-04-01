namespace Ratelite;

public class Scene
{
	private readonly List<IPlugin> plugins = [];
	
	public bool isRunning { get; private set; }
	
	public virtual void Init() { }
	public virtual void Start() { }
	public virtual void Update() { }
	public virtual void Render() { }
	
	public virtual Task Load() => Task.CompletedTask;
	public virtual void Unload() { }
	
	public T AddPlugin<T>() where T : class, IPlugin, new()
	{
		var instance = Activator.CreateInstance<T>();
		plugins.Add(instance);
		return instance;
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
	}
}