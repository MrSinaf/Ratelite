namespace Ratelite;

public static class Stage
{
	public static Scene current { get; private set; } = new ();
	public static Scene loadingScene = new ();
	
	public static void Load(Scene scene)
	{
		current = loadingScene;
		Task.Run(async () =>
			{
				current.InternalUnload();
				await scene.Load();
			}
		).ContinueWith(_ => current = scene);
	}
}