namespace Ratelite.GO;

public class World : IPlugin
{
	private readonly List<RObject> objects = [];
	private readonly List<Camera> cameras = [];
	
	public Camera camera => cameras.Last();
	public RObject this[int index] => objects[index];
	public RObject? this[string name] => objects.Find(obj => obj.name == name);
	
	public void Init()
		=> AddCamera(new Camera());
	
	public void AddCamera(Camera camera)
	{
		camera.world = this;
		cameras.Add(camera);
		UpdateCameraPriorities();
	}
	
	public void RemoveCamera(Camera camera)
	{
		camera.world = null;
		cameras.Remove(camera);
	}
	
	public void UpdateCameraPriorities()
		=> cameras.Sort((a, b) => a.priority.CompareTo(b.priority));
	
	public void Update()
	{
		for (var i = 0; i < objects.Count; i++)
		{
			var obj = objects[i];
			
			if (obj.isDestroyed)
			{
				objects.RemoveAt(i--);
				continue;
			}
			
			if (!obj.isActif)
				continue;
			
			obj.InternalUpdate();
		}
	}
	
	public void Render()
	{
		foreach (var camera in cameras)
		{
			if (camera.actif)
				camera.Render(objects);
		}
	}
	
	public void Destroy()
	{
		foreach (var camera in cameras)
			camera.Destroy();
		foreach (var obj in objects)
			obj.Destroy();
	}
	
	public void AddObject(RObject obj) => objects.Add(obj);
	public void RemoveObject(RObject obj) => objects.Remove(obj);
}