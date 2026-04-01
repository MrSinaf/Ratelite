using System.Reflection;
using Ratelite.Rendering;

namespace Ratelite;

public static class R
{
	public static GameWindow game { get; private set; } = null!;
	internal static RawImage icon { get; private set; } = null!;
	public static bool isRunning { get; private set; }
	
	public static RConfig CreateGame(string? gameName = null)
	{
		AppDomain.CurrentDomain.AssemblyResolve -= ResolveAssembly;
		AppDomain.CurrentDomain.AssemblyResolve += ResolveAssembly;
		return new RConfig { windowOptions = new WindowOptions(gameName ?? "RGame", 1280, 720) };
	}
	
	public static void RunGame(RConfig config)
	{
		if (isRunning)
			throw new Exception("Game is already running! o((>ω< ))o");
		
		AppDomain.CurrentDomain.AssemblyResolve -= ResolveAssembly;
		AppDomain.CurrentDomain.AssemblyResolve += ResolveAssembly;
		
		Console.ForegroundColor = (ConsoleColor)(Random.Shared.Next(4) + 10);
		Console.WriteLine($"Ratelite v{GetEngineVersion()}\n  by PurrVert Studio\n");
		Console.ResetColor();
		
		isRunning = true;
		
		icon = GetApplicationIcon();
		var splash = new SplashWindow(config);
		splash.window.Run();
		
		if (splash.isLoaded)
		{
			game = new GameWindow(config, splash);
			splash.Destroy();
			game.window.Run();
		}
	}
	
	private static RawImage GetApplicationIcon()
	{
		using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(
			"Ratelite.assets.textures.icon-r.png"
		)!;
		return RawImage.Load(stream);
	}
	
	public static string GetEngineVersion()
	{
		var infoVersion = Assembly.GetAssembly(typeof(R))!
								  .GetCustomAttribute<AssemblyInformationalVersionAttribute>()!
								  .InformationalVersion.Split('+')[0];
		var versionDetails = infoVersion.Split('-');
		return versionDetails[0] + versionDetails.Length switch
		{
			2 => $" - [{versionDetails[1]}]",
			3 => $" - [{versionDetails[1]} {versionDetails[2]}]",
			_ => string.Empty
		};
	}
	
	private static Assembly? ResolveAssembly(object? sender, ResolveEventArgs args)
	{
		var assemblyPath = Path.Combine(
			AppDomain.CurrentDomain.BaseDirectory,
			"runtimes",
			new AssemblyName(args.Name).Name + ".dll"
		);
		return !File.Exists(assemblyPath) ? null : Assembly.LoadFrom(assemblyPath);
	}
}