namespace Ratelite.Debugs;

public static class RDebug
{
	public static bool showMenuBar = true;
	
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
		}
		ImGui.EndMainMenuBar();
		ImGui.PopStyleVar();
		ImGui.PopStyleColor();
	}
}