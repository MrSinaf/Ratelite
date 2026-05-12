using Ratelite.Resources;
using Ratelite.Utils;

namespace Ratelite.GO;

public class Camera
{
	private static readonly Comparison<RObject> drawOrderComparison =
			(a, b) => a.drawOrder.CompareTo(b.drawOrder);
	
	private readonly CameraUniform uniform = new ();
	private readonly Mesh mesh;
	
	public World? world { get; internal set; }
	public Vector2 resolution { get; private set; }
	public Vector2 halfResolution { get; private set; }
	public RenderTexture renderTexture { get; private set; } = null!;
	
	public bool actif = true;
	public bool visible = true;
	public Material material;
	public Vector2 position;
	
	public Color backgroundColor
	{
		get => renderTexture.clearColor;
		set => renderTexture.clearColor = value;
	}
	public float zoom
	{
		get;
		set
		{
			field = float.Clamp(value, 0.00001F, 1000000F);
			UpdateZoom();
		}
	} = 1;
	public int priority
	{
		get;
		set
		{
			field = value;
			world?.UpdateCameraPriorities();
		}
	}
	
	public Camera()
	{
		var window = R.game.window;
		window.resized += OnWindowResized;
		
		mesh = MeshFactory.CreateQuad(Vector2.one);
		material = new Material(Vault.GetAsset<Shader>(GOModule.CAMERA_SHADER)!);
		UpdateZoom();
		UpdateRenderTexture();
	}
	
	internal void Render(List<RObject> objects)
	{
		uniform.projection = Matrix3X3.CreateTranslation(-position) *
							 Matrix3X3.CreateOrthographic(resolution.x, resolution.y);
		uniform.deltaTime = Time.delta;
		uniform.time = Time.total;
		uniform.UpdateBuffer();
		
		objects.Sort(drawOrderComparison);
		
		renderTexture.Bind();
		{
			foreach (var obj in objects)
				obj.Render();
		}
		renderTexture.Unbind();
		
		if (visible)
		{
			material.ApplyProperties();
			mesh.Draw();
		}
	}
	
	public Vector2 ScreenToWorldPosition(Vector2 screenPosition)
		=> position + screenPosition / zoom - halfResolution;
	
	public Vector2 WorldToScreenPosition(Vector2 worldPosition)
		=> (worldPosition - position) * zoom + halfResolution * zoom;
	
	private void UpdateZoom()
	{
		resolution = R.game.window.size / zoom;
		halfResolution = resolution * 0.5F;
		material.SetProperty("u_model", Matrix3X3.CreateScale(resolution));
	}
	
	private void OnWindowResized(Vector2Int size)
	{
		if (size != Vector2.zero)
		{
			renderTexture.Dispose();
			UpdateRenderTexture();
			UpdateZoom();
		}
	}
	
	private void UpdateRenderTexture()
	{
		renderTexture = new RenderTexture(
			(uint)R.game.window.size.x,
			(uint)R.game.window.size.y
		);
		material.SetProperty("u_texture", renderTexture);
		material.SetProperty(
			"u_projection",
			Matrix3X3.CreateTranslation(new Vector2(0, 0)) *
			Matrix3X3.CreateOrthographic(resolution.x, resolution.y, false)
		);
	}
	
	internal void Destroy()
	{
		MainThread.Enqueue(() =>
			{
				renderTexture.Dispose();
				mesh.Dispose();
			}
		);
		R.game.window.resized -= OnWindowResized;
	}
}