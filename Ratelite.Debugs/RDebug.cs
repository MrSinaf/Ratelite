using System.Reflection;
using Ratelite.Debugs.Windows;

namespace Ratelite.Debugs;

public static class RDebug
{
	public static Dictionary<string, IDebugWindow> windows = [];
	public static bool showMenuBar = true;
	
	private static readonly Type[] scenes;
	
	public static event Action onMainMenuBar = delegate { };
	
	static RDebug()
	{
		scenes = Assembly.GetEntryAssembly()?.GetTypes()
						 .Where(t => t.IsSubclassOf(typeof(Scene))).ToArray() ?? [];
		
		windows["vault"] = new VaultDebugWindow();
		windows["logs"] = new LogDebugWindow();
	}
	
	internal static void Render()
	{
		if (!showMenuBar)
			return;
		
		ImGui.PushStyleColor(ImGuiCol.MenuBarBg, R.game.windowColor);
		ImGui.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 0);
		ImGui.BeginMainMenuBar();
		{
			if (ImGui.BeginMenu("Ratelite"))
			{
				if (ImGui.BeginMenu("Display"))
				{
					if (ImGui.MenuItem("Fullscreen"))
						R.game.window.displayMode = Window.DisplayMode.Fullscreen;
					
					if (ImGui.MenuItem("No border"))
						R.game.window.displayMode = Window.DisplayMode.NoBorder;
					
					if (ImGui.MenuItem("Windowed"))
						R.game.window.displayMode = Window.DisplayMode.Window;
					
					ImGui.EndMenu();
				}
				
				if (ImGui.MenuItem("Exit"))
					R.game.window.Close();
				
				ImGui.EndMenu();
			}
			
			if (ImGui.BeginMenu("Windows"))
			{
				if (ImGui.MenuItem("Vault"))
				{
					if (!windows.TryGetValue("vault", out var window))
						windows.Add("vault", window = new VaultDebugWindow());
					window.show = !window.show;
				}
				
				if (ImGui.MenuItem("Logs"))
				{
					if (!windows.TryGetValue("logs", out var window))
						windows.Add("logs", window = new LogDebugWindow());
					window.show = !window.show;
				}
				
				ImGui.EndMenu();
			}
			
			if (ImGui.BeginMenu("Scene"))
			{
				if (ImGui.MenuItem("Restart"))
					Stage.Load((Scene)Activator.CreateInstance(Stage.current.GetType())!);
				
				if (ImGui.BeginMenu("Change to"))
				{
					foreach (var scene in scenes)
						if (ImGui.MenuItem(scene.Name))
							Stage.Load((Scene)Activator.CreateInstance(scene)!);
					ImGui.EndMenu();
				}
				ImGui.EndMenu();
			}
			
			onMainMenuBar.Invoke();
		}
		ImGui.EndMainMenuBar();
		ImGui.PopStyleVar();
		ImGui.PopStyleColor();
		
		foreach (var (_, window) in windows)
			window.Draw();
	}
}