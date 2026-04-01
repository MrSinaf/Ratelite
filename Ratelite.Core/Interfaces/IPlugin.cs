namespace Ratelite;

public interface IPlugin
{
	public void Init();
	public void Update();
	public void Render();
	public void Destroy();
}