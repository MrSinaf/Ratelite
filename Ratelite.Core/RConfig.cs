using Ratelite.Bindings;
using Ratelite.Rendering;

namespace Ratelite;

public class RConfig
{
	public required WindowOptions windowOptions;
	public RawImage? icon;
	public Type? startingScene;
	public event Func<IProgress<float>, Task> action = delegate { return Task.CompletedTask; };
	
	internal readonly List<IModule> modules = [];
	
	public RConfig AddModule<T>() where T : IModule
	{
		modules.Add(Activator.CreateInstance<T>());
		return this;
	}
	
	public RConfig SetIcon(string path)
	{
		if (!File.Exists(path))
			Log.Warning("Icon file not found: " + path);
		else
			SetIcon(File.OpenRead(path));
		return this;
	}
	
	public RConfig SetWindowOptions(WindowOptions options)
	{
		windowOptions = options;
		return this;
	}
	
	public RConfig SetIcon(Stream stream)
	{
		var icon = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
		this.icon = new RawImage(icon.width, icon.height, icon.data);
		return this;
	}
	
	public RConfig SetStartingScene<T>() where T : Scene, new()
	{
		startingScene = typeof(T);
		return this;
	}
	
	public RConfig LoadingAssets(Func<IProgress<float>, Task> action)
	{
		this.action = action;
		return this;
	}
	
	public void Run() => R.RunGame(this);
	
	internal async Task Action(IProgress<float> progress) => await action(progress);
}