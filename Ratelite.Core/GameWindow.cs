using Ratelite.Platforms;
using Ratelite.Utils;

namespace Ratelite;

public class GameWindow
{
	public readonly Window window;
	
	private readonly IModule[] modules;
	private readonly List<IUpdatableModule> updatables = [];
	private readonly List<IRenderableModule> renderables = [];
	
#if UseXYDebug
	private ImGuiController imGuiController = null!;
#endif
	
	public Color windowColor
	{
		get;
		set
		{
			field = value;
			window.SetWindowColor(field);
#if UseXYDebug
			XYDebug.mainMenuBarColor = field;
#endif
		}
	}
	
	public Color backgroundColor
	{
		get;
		set
		{
			field = value;
			GL.ClearColor(field);
		}
	}
	
	internal GameWindow(RConfig config, SplashWindow splash)
	{
		modules = config.modules.OrderByDescending(x => x.priority).ToArray();
		
		window = Window.Create(
			config.windowOptions with { visible = false },
			splash.window
		);
		window.SetIcon(config.icon ?? R.icon);
		window.Center();
		
		window.start += Start;
		window.update += Update;
		window.render += Render;
		window.resized += Resized;
	}
	
	public bool HasModule<T>() where T : IModule => modules.Any(x => x is T);
	
	private void Start()
	{
		window.Show();
		window.Focus();
		windowColor = new Color(0x122333);
		backgroundColor = new Color(0x0B1A28);
		
		foreach (var module in modules)
		{
			try
			{
				module.Init();
				switch (module)
				{
					case IUpdatableModule updatable:
						updatables.Add(updatable);
						break;
					case IRenderableModule renderable:
						renderables.Add(renderable);
						break;
				}
			}
			catch (Exception e)
			{
				Log.Write($"Error intializing module (ㆆ_ㆆ) : {module.GetType().Name}", e);
			}
		}
#if UseXYDebug
		imGuiController = new ImGuiController(window);
		Log.Write(
			"Dear ImGui initialized! ヾ(•ω•`)o\n	Press 'F1' to open the main bar.",
			Log.Level.Info
		);
#endif
	}
	
	private void Update()
	{
		foreach (var module in updatables)
		{
			try
			{
				module.Update();
			}
			catch (Exception e)
			{
				Log.Write($"Update error in {module.GetType().Name}", e);
			}
		}
		
		try
		{
			Stage.current.InternalUpdate();
			MainThreadQueue.ExecuteAll();
#if UseXYDebug
			imGuiController.Update();
#endif
		}
		catch (Exception e)
		{
			Log.Write("Update error (ﾟДﾟ*)ﾉ", e);
		}
	}
	
	private void Render()
	{
		foreach (var module in renderables)
		{
			try
			{
				module.Render();
			}
			catch (Exception e)
			{
				Log.Write($"Render error in {module.GetType().Name}", e);
			}
		}
		
		try
		{
			Stage.current.InternalRender();
			MainThreadQueue.ExecuteAllRenderer();
#if UseXYDebug
			imGuiController.Render();
#endif
		}
		catch (Exception e)
		{
			Log.Write("Render error ＼（〇_ｏ）／", e);
		}
	}
	
	private void Resized(Vector2Int size) { }
}