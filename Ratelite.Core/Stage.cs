namespace Ratelite;

public static class Stage
{
	public static Scene current { get; private set; } = new ();
	public static Scene loadingScene = new ();
	
	public static event SceneChanged onSceneChanged = delegate { };
	
	public static void Load(Scene scene)
	{
		var oldScene = current;
		current = loadingScene;
		Task.Run(async () =>
			{
				oldScene.InternalUnload();
				await scene.Load();
			}
		).ContinueWith(t =>
		{
			if (t.Exception != null)
				MainThread.Enqueue(() => throw t.Exception);
			
			onSceneChanged(oldScene, scene);
			current = scene;
		});
	}
}


public delegate void SceneChanged(Scene oldScene, Scene newScene);