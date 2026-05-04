namespace Ratelite.Debugs;

public class DebugModule : IRenderablePhaseModule, IRenderableModule
{
	public int priority => int.MinValue;
	
	private ImGuiController controller = null!;
	
	public void Init()
	{
		controller = new ImGuiController(R.game.window);
	}
	
	public void BeginRender()
	{
		controller.BeginRender();
	}
	
	public void Render()
	{
		RDebug.Render();
	}
	
	public void EndRender()
	{
		controller.EndRender();
	}
}