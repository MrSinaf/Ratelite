namespace Ratelite.GO;

public class World : IPlugin
{
	private readonly List<RObject> objects = [];
	public readonly Camera camera = new ();
	
	public RObject this[int index] => objects[index];
	public RObject? this[string name] => objects.Find(obj => obj.name == name); 
	
	public void Init()
	{
		
	}
	
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
		camera.Render(objects);
	}
	
	public void Destroy()
	{
		camera.Destroy();
		foreach (var obj in objects)
			obj.Destroy();
	}
	
	public void AddObject(RObject obj) => objects.Add(obj);
	public void RemoveObject(RObject obj) => objects.Remove(obj);
}