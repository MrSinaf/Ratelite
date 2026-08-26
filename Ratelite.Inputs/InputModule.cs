namespace Ratelite.Inputs;

public class InputModule : IUpdatableModule
{
	public int priority => 0;
	
	public void Init() => Input.Init();
	
	public void Update() => Input.Update();
}