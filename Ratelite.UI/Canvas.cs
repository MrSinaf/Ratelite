namespace Ratelite.UI;

public class Canvas : IPlugin
{
	private readonly CanvasUniform uniform = new ();
	private readonly UIElement tree = new ();
	
	public RootElement root { get; private set; } = null!;
	
	public void Init()
	{
		var window = R.game.window;
		window.resized += OnWindowResized;
		
		root = new RootElement { size = window.size };
		uniform.projection = Matrix3X3.CreateOrthographic(root.size.x, root.size.y, false);
		uniform.resolution = root.size;
	}
	
	public void Update()
	{
		root.InternalUpdate(tree);
	}
	
	public void Render()
	{
		uniform.deltaTime = Time.delta;
		uniform.time = Time.total;
		uniform.UpdateBuffer();
		
		root.InternalRender();
	}
	
	public void Destroy()
	{
		root.Destroy();
		R.game.window.resized -= OnWindowResized;
	}
	
	private void OnWindowResized(Vector2Int size)
	{
		if (size.x <= 0 || size.y <= 0)
			return;
		
		root.size = uniform.resolution = size;
		uniform.projection = Matrix3X3.CreateOrthographic(root.size.x, root.size.y, false);
	}
}