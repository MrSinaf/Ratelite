namespace Ratelite.GO;

public class Camera
{
	private static readonly Comparison<RObject> drawOrderComparison =
			(a, b) => a.drawOrder.CompareTo(b.drawOrder);
	
	private readonly CameraUniform uniform = new ();
	
	public Vector2 resolution { get; private set; }
	public Vector2 halfResolution { get; private set; }
	
	public Vector2 position;
	
	public float zoom
	{
		get;
		set
		{
			field = float.Clamp(value, 0.00001F, 1000000F);
			UpdateZoom();
		}
	} = 1;
	
	public Camera()
	{
		var window = R.game.window;
		window.resized += OnWindowResized;
		UpdateZoom();
	}
	
	internal void Render(List<RObject> objects)
	{
		uniform.projection = Matrix3X3.CreateTranslation(-position) *
							 Matrix3X3.CreateOrthographic(resolution.x, resolution.y) ;
		uniform.deltaTime = Time.delta;
		uniform.time = Time.total;
		uniform.UpdateBuffer();
		
		objects.Sort(drawOrderComparison);
		
		foreach (var obj in objects)
			obj.Render();
	}
	
	public Vector2 ScreenToWorldPosition(Vector2 screenPosition)
		=> position + screenPosition / zoom - halfResolution;
	
	public Vector2 WorldToScreenPosition(Vector2 worldPosition)
		=> (worldPosition - position) * zoom + halfResolution * zoom;
	
	private void UpdateZoom()
	{
		resolution = R.game.window.size / zoom;
		halfResolution = resolution * 0.5F;
	}
	
	private void OnWindowResized(Vector2Int size)
	{
		if (size != Vector2.zero)
			UpdateZoom();
	}
	
	internal void Destroy()
	{
		R.game.window.resized -= OnWindowResized;
	}
}