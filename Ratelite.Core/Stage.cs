namespace Ratelite;

public static class Stage
{
	public static Scene current { get; private set; } = new ();
	
	public static async Task Load(Scene scene)
	{
		current.InternalUnload();
		current = scene;
		await scene.Load();
	}
}