using System.Reflection;
using Ratelite.Rendering;

namespace Ratelite;

public static class R
{
	public static GameWindow game { get; private set; } = null!;
	public static bool isRunning { get; private set; }
	
	internal static RawImage icon { get; private set; } = null!;
	
	/// <summary>
	///	Crée une configuration de jeu qui initialise les options de la fenêtre du jeu.<br/>
	///	(๑˃ᴗ˂)ﻭ
	/// </summary>
	/// <param name="gameName">
	///	Le nom du jeu à afficher dans la fenêtre (^・ω・^ ). Si null, un nom	par défaut "RGame"
	/// est utilisé.
	/// </param>
	/// <return>
	///	Retourne une instance de <see cref="RConfig"/> contenant les options de	configuration du
	/// jeu.
	/// </return>
	public static RConfig CreateGame(string? gameName = null)
	{
		AppDomain.CurrentDomain.AssemblyResolve -= ResolveAssembly;
		AppDomain.CurrentDomain.AssemblyResolve += ResolveAssembly;
		return new RConfig { windowOptions = new WindowOptions(gameName ?? "RGame", 1280, 720) };
	}
	
	/// <summary>
	/// Lance le jeu (≧▽≦)ﾉ en utilisant la configuration fournie.
	/// </summary>
	/// <param name="config">
	/// La configuration de jeu <see cref="RConfig"/> contenant les paramètres nécessaires, comme
	/// les options de la fenêtre, l'icône et la scène de départ et surtout les modules.
	/// </param>
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
			MainThread.Enqueue(() =>
					Stage.Load(
						(Scene)Activator.CreateInstance(config.startingScene ?? typeof(Scene))!
					)
			);
			game.window.Run();
		}
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
	
	private static RawImage GetApplicationIcon()
	{
		using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(
			"Ratelite.assets.textures.icon-r.png"
		)!;
		return RawImage.Load(stream);
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