using Ratelite.Resources;

namespace Ratelite.GO;

public class RObject
{
	private readonly List<Component> components = [];
	
	public bool enable = true;
	public bool isDestroyed { get; private set; }
	public bool isActif => enable && !isDestroyed;
	public bool canDraw => material != null && mesh is { isValid: true };
	public int drawOrder;
	
	public string name;
	public Mesh? mesh;
	public Material? material;
	
	private bool dirtyMatrix;
	
	public Vector2 position
	{
		get;
		set
		{
			field = value;
			dirtyMatrix = true;
		}
	} = Vector2.zero;
	public Vector2 scale
	{
		get;
		set
		{
			field = value;
			dirtyMatrix = true;
		}
	} = Vector2.one;
	public float rotation
	{
		get;
		set
		{
			field = value;
			dirtyMatrix = true;
		}
	}
	public Matrix3X3 matrix
	{
		get
		{
			if (dirtyMatrix)
			{
				field = Matrix3X3.CreateRotation(float.DegreesToRadians(rotation)) * 
						Matrix3X3.CreateScale(scale) * 
						Matrix3X3.CreateTranslation(position);
				dirtyMatrix = false;
			}
			
			return field;
		}
	} = Matrix3X3.Identity();
	
	public RObject() => name = GetType().Name;
	
	public T AddComponent<T>() where T : Component
	{
		var component = Activator.CreateInstance<T>();
		component.AddOwner(this);
		components.Add(component);
		return component;
	}
	
	public void RemoveComponent<T>(T component) where T : Component
	{
		if (components.Remove(component))
			component.RemoveOwner();
	}
	
	public T? GetComponent<T>() where T : Component => components.OfType<T>().FirstOrDefault();
	
	public T[] GetComponents<T>() where T : Component => components.OfType<T>().ToArray();
	
	public void Destroy()
	{
		foreach (var component in components)
			component.Destroy();
		components.Clear();
		isDestroyed = true;
		Destroyed();
	}
	
	protected internal virtual void Update() { }
	protected virtual void Destroyed() { }
	
	internal void Render()
	{
		if (!isActif || !canDraw)
			return;
		
		material.ApplyProperties();
		material.shader.gProgram.SetUniform("u_model", matrix);
		mesh.Draw();
	}
	
	internal void InternalUpdate()
	{
		
	}
}