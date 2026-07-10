namespace Ratelite.Debugs.Windows;

public interface IDebugWindow
{
	public bool show { get; set; }
	public void Draw();
}