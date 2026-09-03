using Ratelite.Resources;
using Ratelite.Utils;

namespace Ratelite.GO;

public class Camera
{
	private readonly CameraUniform uniform = new ();
	private readonly Mesh mesh;
	
	public bool actif = true;
	public bool visible = true;
	public Material material;
	public Vector2 position;
	
	public event Action<float> onZoomChanged = delegate {};
	
	public World? world { get; internal set; }
	public Vector2 halfResolution { get; private set; }
	public RenderTexture renderTexture { get; private set; } = null!;
	public float displayScale { get; private set; }
	public float zoomScaled { get; private set; }
	
	private Vector2 displaySize;
	private Vector2 displayPosition;
	private Vector2 resolutionCalculated;
	
	public Vector2 resolution
	{
		get;
		set
		{
			field = value;
			UpdateRenderTexture();
		}
	} = new (960, 540);
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
		
		UpdateRenderTexture();
	}
	
	internal void Render(List<RObject> objects)
	{
		var snappedPosition = (position * zoom).ToVector2Int() / zoom;
		uniform.projection = Matrix3X3.CreateTranslation(-snappedPosition) *
							 Matrix3X3.CreateOrthographic(
								 resolutionCalculated.x,
								 resolutionCalculated.y
							 );
		uniform.deltaTime = Time.delta;
		uniform.time = Time.total;
		uniform.UpdateBuffer();
		
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
		=> position + ((screenPosition - displayPosition) 
			/ displaySize * resolution - halfResolution) / zoom;
	
	public Vector2 WorldToScreenPosition(Vector2 worldPosition)
		=> ((worldPosition - position) * zoom + halfResolution)
				/ resolution * displaySize + displayPosition;
	
	private void UpdateZoom()
	{
		resolutionCalculated = resolution / zoom;
		halfResolution = resolution * 0.5F;
		zoomScaled = displayScale * zoom;
		onZoomChanged.Invoke(zoom);
	}
	
	private void OnWindowResized(Vector2Int size)
	{
		if (size.x <= 0 || size.y <= 0)
			return;
		
		UpdateScreenProjection();
	}
	
	private void UpdateRenderTexture()
	{
		renderTexture = new RenderTexture(
			(uint)resolution.x,
			(uint)resolution.y
		);
		
		material.SetProperty("u_texture", renderTexture);
		UpdateScreenProjection();
	}
	
	private void UpdateScreenProjection()
	{
		var frameBufferSize = R.game.window.frameBufferSize.ToVector2();
		
		displayScale = float.Ceiling(MathF.Max(
			frameBufferSize.x / resolution.x,
			frameBufferSize.y / resolution.y
		));
		
		displaySize = resolution * displayScale;
		displayPosition = (frameBufferSize - displaySize) * 0.5F;
		
		material.SetProperty(
			"u_projection",
			Matrix3X3.CreateOrthographic(frameBufferSize.x, frameBufferSize.y, false)
		);
		
		material.SetProperty(
			"u_model",
			Matrix3X3.CreateScale(displaySize) *
			Matrix3X3.CreateTranslation(displayPosition)
		);
		UpdateZoom();
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