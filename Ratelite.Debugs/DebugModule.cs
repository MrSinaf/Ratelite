namespace Ratelite.Debugs;

public class DebugModule : IRenderableModule, IUpdatableModule
{
	public int priority => int.MinValue;
	
	private ImGuiController controller = null!;
	
	public void Init()
	{
		controller = new ImGuiController(R.game.window);
	}
	
	public void Update()
	{
		controller.Update();
	}
	
	public void Render()
	{
		controller.Render();
	}
}