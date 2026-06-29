namespace Ratelite;

public class ComponentSystem : ComponentSystem<IComponent>;

public class ComponentSystem<T> where T : class, IComponent
{
	private readonly List<T> components = [];
	private readonly List<IRenderableComponent> renderableComponents = [];
	private readonly List<IUpdatableComponent> updatableComponents = [];
	
	public TC AddComponent<TC>() where TC : T, new()
	{
		var component = new TC();
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
		
		return component;
	}
	
	public void RemoveComponent<TC>(TC component) where TC : T
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
	
	public IEnumerable<TC> GetComponents<TC>() where TC : T
		=> components.OfType<TC>();
	
	public TC? GetComponent<TC>() where TC : T
		=> components.OfType<TC>().FirstOrDefault();
	
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
		{
			component.enable = false;
			if (component is IDisposableComponent disposable)
				disposable.Dispose();
		}
	}
}