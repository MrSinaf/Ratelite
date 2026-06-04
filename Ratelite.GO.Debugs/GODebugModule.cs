using Ratelite.Debugs;

namespace Ratelite.GO.Debugs;

public class GODebugModule : IRenderableModule
{
	public int priority => 0;
	
	public void Init()
	{
		RDebug.onMainMenuBar += MainMenuBar;
	}
	
	private void MainMenuBar()
	{
		if (ImGui.BeginMenu("World"))
		{
			
			ImGui.EndMenu();
		}
	}
	
	public void Render()
	{
		
	}
}