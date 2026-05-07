namespace Ratelite;

public class ComponentSystem
{
	private readonly List<IComponent> components = [];
	private readonly List<IRenderableComponent> renderableComponents = [];
	private readonly List<IUpdatableComponent> updatableComponents = [];
	
	public void AddComponent<T>() where T : IComponent, new()
	{
		var component = new T();
		components.Add(component);
		
		component.enable = true;
		switch (component)
		{
			case IRenderableComponent renderable:
				renderableComponents.Add(renderable);
				break;
			case IUpdatableComponent updatable:
				updatableComponents.Add(updatable);
				break;
		}
	}
	
	public void RemoveComponent<T>(T component) where T : IComponent
	{
		components.Remove(component);
		
		switch (component)
		{
			case IRenderableComponent renderable:
				renderableComponents.Remove(renderable);
				break;
			case IUpdatableComponent updatable:
				updatableComponents.Remove(updatable);
				break;
		}
	}
	
	public void Update()
	{
		foreach (var updatable in updatableComponents)
			if (updatable.enable)
				updatable.Update();
	}
	
	public void Render()
	{
		foreach (var renderable in renderableComponents)
			if (renderable.enable)
				renderable.Render();
	}
	
	public void Destroy()
	{
		foreach (var component in components)
			if (component is IDisposableComponent disposable)
				disposable.Dispose();
	}
}